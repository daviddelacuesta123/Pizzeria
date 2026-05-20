namespace Pizzeria.Domain.Productos.Decorators;

using Pizzeria.Domain.Productos;

public class Champinones : PizzaDecorator
{
    private readonly double _precioChampinones;

    public Champinones(Pizza pizza, double precioChampinones = 3500) : base(pizza)
    {
        _precioChampinones = precioChampinones;
        Nombre = pizza.Nombre + " + Champiñones";
    }

    public override double CalcularPrecio() => base.CalcularPrecio() + _precioChampinones;
}
