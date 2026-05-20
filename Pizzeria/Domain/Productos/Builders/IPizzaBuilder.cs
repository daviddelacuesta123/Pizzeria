namespace Pizzeria.Domain.Productos.Builders;

using Pizzeria.Domain.Productos;

public interface IPizzaBuilder
{
    IPizzaBuilder SetTamano(string tamano);
    IPizzaBuilder SetIngredientes(List<Ingrediente> ings);
    IPizzaBuilder SetPrecioBase(double precio);
    Pizza Build();
}
