namespace Pizzeria.Patrones.States;

using Pizzeria.Domain.Productos;

public class PizzaListaParaEntregar : IEstadoPizza
{
    public void ManejarEstado(Pizza pizza)
    {
        Console.WriteLine($"[Estado] Pizza lista → en camino");
        pizza.CambiarEstado(new PizzaEnCamino());
    }

    public string GetNombre() => "Lista Para Entregar";
}
