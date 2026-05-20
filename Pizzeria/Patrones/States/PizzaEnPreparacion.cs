namespace Pizzeria.Patrones.States;

using Pizzeria.Domain.Productos;

public class PizzaEnPreparacion : IEstadoPizza
{
    public void ManejarEstado(Pizza pizza)
    {
        Console.WriteLine($"[Estado] Pizza en preparación → pasa al Horno");
        pizza.CambiarEstado(new PizzaEnHorno());
    }

    public string GetNombre() => "En Preparación";
}
