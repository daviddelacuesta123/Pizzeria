namespace Pizzeria.Patrones.States;

using Pizzeria.Domain.Productos;

public class PizzaEnCamino : IEstadoPizza
{
    public void ManejarEstado(Pizza pizza)
    {
        Console.WriteLine($"[Estado] Pizza entregada al cliente");
    }

    public string GetNombre() => "En Camino";
}
