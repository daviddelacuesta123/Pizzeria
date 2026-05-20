namespace Pizzeria.Servicios;

using Pizzeria.Domain.Facturacion;
using Pizzeria.Domain.Pedidos;
using Pizzeria.Domain.Clientes;
using Pizzeria.Servicios.Interfaces;
using Pizzeria.Repositorios.Interfaces;

public class FacturaService : IFacturaService
{
    private readonly IFacturaRepository _repo;
    private int _siguienteId = 1;

    public FacturaService(IFacturaRepository repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public Factura GenerarFactura(IPedido pedido, ResolucionDIAN resolucion, string nitEmpresa,
                                  Cliente cliente, string nitCliente)
    {
        if (pedido == null) throw new ArgumentNullException(nameof(pedido));
        if (!resolucion.EstaVigente())
            throw new InvalidOperationException("La resolución DIAN no está vigente");
        var factura = new Factura(_siguienteId++, pedido, resolucion, nitEmpresa, cliente, nitCliente);
        _repo.Agregar(factura);
        return factura;
    }

    public bool ValidarFacturaDIAN(Factura factura) => factura != null && factura.EsValidaDIAN();

    public IReadOnlyList<Factura> GetFacturas() => _repo.ObtenerTodos();
}
