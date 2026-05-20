namespace Pizzeria.Domain.Productos.Builders;

using Pizzeria.Domain.Productos;

public class PizzaEstofadaBuilder : IPizzaBuilder
{
    private Estofada _pizza = new();

    public IPizzaBuilder SetTamano(string tamano) { _pizza.SetTamano(tamano); return this; }
    public IPizzaBuilder SetIngredientes(List<Ingrediente> ings) { _pizza.SetIngredientes(ings); return this; }
    public IPizzaBuilder SetPrecioBase(double precio) { _pizza.SetPrecioBase(precio); return this; }

    public Pizza Build()
    {
        var resultado = _pizza;
        _pizza = new Estofada();
        return resultado;
    }
}
