namespace Pizzeria.Patrones.Command;

using Pizzeria.Domain.Productos;
using Pizzeria.Patrones.States;

public class PrepararPizzaCommand : ICommand
{
    private readonly Pizza _pizza;
    private readonly Cocina _cocina;
    private IEstadoPizza? _estadoAnterior;

    public PrepararPizzaCommand(Pizza pizza, Cocina cocina)
    {
        _pizza = pizza;
        _cocina = cocina;
    }

    public void Ejecutar()
    {
        _estadoAnterior = _pizza.GetEstado();
        Console.WriteLine($"[Command] Preparando pizza...");
        _pizza.GetEstado()?.ManejarEstado(_pizza);
    }

    public void Deshacer()
    {
        if (_estadoAnterior != null)
        {
            Console.WriteLine($"[Command] Deshaciendo preparación, volviendo a: {_estadoAnterior.GetNombre()}");
            _pizza.CambiarEstado(_estadoAnterior);
        }
    }
}
