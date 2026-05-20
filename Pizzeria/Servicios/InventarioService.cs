namespace Pizzeria.Servicios;

using Pizzeria.Domain.Inventario;
using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Productos;
using Pizzeria.Patrones.Observer;
using Pizzeria.Eventos;
using Pizzeria.Repositorios.Interfaces;

public class InventarioService
{
    private readonly IOrdenCompraRepository _repoOrdenes;
    private int _siguienteOrdenId = 1;
    private readonly List<IObservador<StockBajoEvento>> _observadoresStock = new();

    public InventarioService(IOrdenCompraRepository repoOrdenes)
    {
        _repoOrdenes = repoOrdenes ?? throw new ArgumentNullException(nameof(repoOrdenes));
    }

    public void SuscribirObservador(IObservador<StockBajoEvento> obs)
    {
        if (obs == null) throw new ArgumentNullException(nameof(obs));
        _observadoresStock.Add(obs);
    }

    public bool VerificarDisponibilidad(Insumo insumo, double cantidad, Franquicia f)
    {
        var stock = f.BuscarStock(insumo);
        return stock != null && stock.CantidadActual >= cantidad;
    }

    public void DescontarInsumos(Pizza pizza, Franquicia f)
    {
        foreach (var ing in pizza.GetIngredientes())
        {
            if (ing.InsumoRequerido == null) continue;
            var stock = f.BuscarStock(ing.InsumoRequerido);
            if (stock == null) continue;
            stock.Descontar(1);
            if (stock.EsBajoStock())
            {
                var evento = new StockBajoEvento(stock.Insumo, stock.CantidadActual, f);
                foreach (var obs in _observadoresStock)
                    obs.Actualizar(evento);
            }
        }
    }

    public List<StockInsumo> GenerarAlertasStock(Franquicia f) => f.GetStockBajo();

    public OrdenCompra CrearOrdenCompra(Franquicia f, Distribuidor d, List<ItemOrden> items)
    {
        var orden = new OrdenCompra(_siguienteOrdenId++, f, d);
        foreach (var item in items)
            orden.AgregarItem(item);
        _repoOrdenes.Agregar(orden);
        return orden;
    }

    public IReadOnlyList<OrdenCompra> GetOrdenes() => _repoOrdenes.ObtenerTodos();
}
