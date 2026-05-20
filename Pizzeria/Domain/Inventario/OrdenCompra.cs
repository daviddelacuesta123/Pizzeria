namespace Pizzeria.Domain.Inventario;

using Pizzeria.Domain.Organizacion;

public class OrdenCompra : Pizzeria.Domain.Shared.IAggregateRoot
{
    public int Id { get; private set; }
    public Franquicia Franquicia { get; private set; }
    public Distribuidor Distribuidor { get; private set; }
    public EstadoOrden Estado { get; private set; }
    public DateTime Fecha { get; private set; }
    public double TotalPagar { get; private set; }

    private readonly List<ItemOrden> _items = new();

    public OrdenCompra(int id, Franquicia franquicia, Distribuidor distribuidor)
    {
        Id = id;
        Franquicia = franquicia;
        Distribuidor = distribuidor;
        Estado = EstadoOrden.PENDIENTE;
        Fecha = DateTime.Now;
    }

    public IReadOnlyList<ItemOrden> GetItems() => _items.AsReadOnly();

    public void AgregarItem(ItemOrden item)
    {
        if (Estado != EstadoOrden.PENDIENTE)
            throw new InvalidOperationException("No se pueden agregar items a una orden no pendiente");
        _items.Add(item);
        TotalPagar = CalcularTotal();
    }

    public double CalcularTotal() => _items.Sum(i => i.CalcularSubtotal());

    public void Aprobar()
    {
        if (Estado != EstadoOrden.PENDIENTE)
            throw new InvalidOperationException($"Solo se pueden aprobar órdenes pendientes (actual: {Estado})");
        Estado = EstadoOrden.APROBADA;
    }

    public void MarcarEnviada()
    {
        if (Estado != EstadoOrden.APROBADA)
            throw new InvalidOperationException($"Solo se pueden enviar órdenes aprobadas (actual: {Estado})");
        Estado = EstadoOrden.ENVIADA;
    }

    public void MarcarEntregada()
    {
        if (Estado != EstadoOrden.ENVIADA)
            throw new InvalidOperationException($"Solo se pueden entregar órdenes enviadas (actual: {Estado})");
        Estado = EstadoOrden.ENTREGADA;
    }

    public void Cancelar()
    {
        if (Estado == EstadoOrden.ENTREGADA)
            throw new InvalidOperationException("No se puede cancelar una orden ya entregada");
        Estado = EstadoOrden.CANCELADA;
    }
}
