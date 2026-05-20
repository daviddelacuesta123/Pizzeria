namespace Pizzeria.Domain.Pedidos.Mementos;

public class PedidoMemento
{
    public string EstadoPedido { get; private set; }
    public IReadOnlyList<ItemPedido> Items { get; private set; }
    public DateTime Fecha { get; private set; }
    public double TotalCliente { get; private set; }

    public PedidoMemento(string estadoPedido, List<ItemPedido> items, DateTime fecha, double totalCliente)
    {
        EstadoPedido = estadoPedido;
        Items = items.AsReadOnly();
        Fecha = fecha;
        TotalCliente = totalCliente;
    }

    public string GetEstado() => EstadoPedido;
    public IReadOnlyList<ItemPedido> GetItems() => Items;
    public DateTime GetFecha() => Fecha;
    public double GetTotalCliente() => TotalCliente;
}
