namespace Pizzeria.Domain.Productos.Decorators;

using Pizzeria.Domain.Productos;

public class QuesoExtra : PizzaDecorator
{
    private readonly double _precioQueso;

    public QuesoExtra(Pizza pizza, double precioQueso = 3000) : base(pizza)
    {
        _precioQueso = precioQueso;
        Nombre = pizza.Nombre + " + Queso Extra";
    }

    public override double CalcularPrecio() => base.CalcularPrecio() + _precioQueso;
}
