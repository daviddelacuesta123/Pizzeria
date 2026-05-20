namespace Pizzeria.Domain.Productos.Builders;

using Pizzeria.Domain.Productos;
using Pizzeria.Domain.Organizacion;

public class DirectorPizza
{
    private readonly IPizzaBuilder _builder;
    private readonly Chef _chef;

    public DirectorPizza(IPizzaBuilder builder, Chef chef)
    {
        _builder = builder;
        _chef = chef;
    }

    public Pizza ConstruirEstandar(string tamano)
    {
        var pizza = _builder
            .SetTamano(tamano)
            .SetIngredientes(new List<Ingrediente>())
            .SetPrecioBase(GetPrecioPorTamano(tamano))
            .Build();
        _chef.PrepararPizza(pizza);
        return pizza;
    }

    public Pizza ConstruirPersonalizada(string tamano, List<Ingrediente> ings)
    {
        var pizza = _builder
            .SetTamano(tamano)
            .SetIngredientes(ings)
            .SetPrecioBase(GetPrecioPorTamano(tamano))
            .Build();
        _chef.PrepararPizza(pizza);
        return pizza;
    }

    private double GetPrecioPorTamano(string tamano) => tamano.ToLower() switch
    {
        "personal" => 15000,
        "mediana"  => 25000,
        "grande"   => 35000,
        "familiar" => 45000,
        _          => 25000
    };
}
