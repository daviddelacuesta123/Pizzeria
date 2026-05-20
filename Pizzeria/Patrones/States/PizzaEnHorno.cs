namespace Pizzeria.Patrones.States;

using Pizzeria.Domain.Productos;

public class PizzaEnHorno : IEstadoPizza
{
    public void ManejarEstado(Pizza pizza)
    {
        Console.WriteLine($"[Estado] Pizza en horno → lista para entregar");
        pizza.CambiarEstado(new PizzaListaParaEntregar());
    }

    public string GetNombre() => "En Horno";
}
