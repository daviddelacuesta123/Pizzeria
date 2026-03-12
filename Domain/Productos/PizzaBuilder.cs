using System.Collections.Generic;

namespace Pizzeria.Domain.Productos
{
    public interface PizzaBuilder
    {
        PizzaBuilder SetTamano(string tamano);
        PizzaBuilder SetIngredientes(List<Ingrediente> ingredientes);
        PizzaBuilder SetPrecioBase(double precio);
        Pizza Build();
    }
}
