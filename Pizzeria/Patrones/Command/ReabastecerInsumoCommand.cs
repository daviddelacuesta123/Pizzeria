namespace Pizzeria.Patrones.Command;

using Pizzeria.Domain.Inventario;

public class ReabastecerInsumoCommand : ICommand
{
    private readonly StockInsumo _stockInsumo;
    private readonly double _cantidad;
    private bool _ejecutado;

    public ReabastecerInsumoCommand(StockInsumo stockInsumo, double cantidad)
    {
        _stockInsumo = stockInsumo;
        _cantidad = cantidad;
    }

    public void Ejecutar()
    {
        _stockInsumo.Reabastecer(_cantidad);
        _ejecutado = true;
        Console.WriteLine($"[Command] Reabastecido {_cantidad} {_stockInsumo.Insumo.UnidadMedida} de {_stockInsumo.Insumo.Nombre}");
    }

    public void Deshacer()
    {
        if (!_ejecutado) return;
        _stockInsumo.Descontar(_cantidad);
        Console.WriteLine($"[Command] Deshecho reabastecimiento de {_cantidad} {_stockInsumo.Insumo.UnidadMedida}");
        _ejecutado = false;
    }
}
