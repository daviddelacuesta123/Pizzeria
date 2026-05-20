namespace Pizzeria.Domain.Productos.Decorators;

using Pizzeria.Domain.Productos;

public class Tocineta : PizzaDecorator
{
    private readonly double _precioTocineta;

    public Tocineta(Pizza pizza, double precioTocineta = 4500) : base(pizza)
    {
        _precioTocineta = precioTocineta;
        Nombre = pizza.Nombre + " + Tocineta";
    }

    public override double CalcularPrecio() => base.CalcularPrecio() + _precioTocineta;
}
