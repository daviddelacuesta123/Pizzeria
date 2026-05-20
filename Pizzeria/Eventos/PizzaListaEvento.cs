namespace Pizzeria.Eventos;

using Pizzeria.Domain.Productos;
using Pizzeria.Domain.Pedidos;

public class PizzaListaEvento
{
    public Pizza Pizza { get; private set; }
    public Pedido Pedido { get; private set; }

    public PizzaListaEvento(Pizza pizza, Pedido pedido)
    {
        Pizza = pizza;
        Pedido = pedido;
    }

    public Pizza GetPizza() => Pizza;
    public Pedido GetPedido() => Pedido;
}
