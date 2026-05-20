namespace Pizzeria.Eventos;

using Pizzeria.Domain.Pedidos;

public class PedidoRecibidoEvento
{
    public Pedido Pedido { get; private set; }
    public DateTime Fecha { get; private set; }

    public PedidoRecibidoEvento(Pedido pedido)
    {
        Pedido = pedido;
        Fecha = DateTime.Now;
    }

    public Pedido GetPedido() => Pedido;
    public DateTime GetFecha() => Fecha;
}
