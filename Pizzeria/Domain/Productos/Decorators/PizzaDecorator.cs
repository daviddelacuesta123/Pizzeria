namespace Pizzeria.Domain.Productos.Decorators;

using Pizzeria.Domain.Productos;

public abstract class PizzaDecorator : Pizza
{
    protected Pizza PizzaDecorada { get; private set; }

    protected PizzaDecorator(Pizza pizza)
    {
        PizzaDecorada = pizza;
        Tamano = pizza.Tamano;
    }

    public override void AgregarIngrediente(Ingrediente i) => PizzaDecorada.AgregarIngrediente(i);
    public override double CalcularPrecio() => PizzaDecorada.CalcularPrecio();
}
