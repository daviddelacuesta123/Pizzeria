namespace Pizzeria.Patrones.States;

using Pizzeria.Domain.Productos;

public class PizzaRecibida : IEstadoPizza
{
    public void ManejarEstado(Pizza pizza)
    {
        Console.WriteLine($"[Estado] Pizza recibida → pasa a En Preparación");
        pizza.CambiarEstado(new PizzaEnPreparacion());
    }

    public string GetNombre() => "Recibida";
}
