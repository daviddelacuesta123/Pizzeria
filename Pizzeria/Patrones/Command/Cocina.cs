namespace Pizzeria.Patrones.Command;

using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Productos;
using Pizzeria.Domain.Inventario;

public class Cocina
{
    public Franquicia Franquicia { get; private set; }
    private readonly List<ICommand> _historialComandos = new();

    public Cocina(Franquicia franquicia)
    {
        Franquicia = franquicia;
    }

    public IReadOnlyList<ICommand> GetHistorial() => _historialComandos.AsReadOnly();

    public void EjecutarComando(ICommand cmd)
    {
        if (cmd == null) throw new ArgumentNullException(nameof(cmd));
        cmd.Ejecutar();
        _historialComandos.Add(cmd);
    }

    public void DeshacerUltimo()
    {
        if (_historialComandos.Count == 0)
        {
            Console.WriteLine("[Cocina] No hay comandos para deshacer");
            return;
        }
        var ultimo = _historialComandos[^1];
        ultimo.Deshacer();
        _historialComandos.RemoveAt(_historialComandos.Count - 1);
    }

    public void PrepararPizza(Pizza pizza)
    {
        if (!VerificarInsumos(pizza))
        {
            Console.WriteLine($"[Cocina] Insumos insuficientes para la pizza");
            return;
        }
        var cmd = new PrepararPizzaCommand(pizza, this);
        EjecutarComando(cmd);
    }

    public bool VerificarInsumos(Pizza pizza)
    {
        foreach (var ing in pizza.GetIngredientes())
        {
            if (ing.InsumoRequerido == null) continue;
            var stock = Franquicia.BuscarStock(ing.InsumoRequerido);
            if (stock == null || stock.CantidadActual <= 0) return false;
        }
        return true;
    }
}
