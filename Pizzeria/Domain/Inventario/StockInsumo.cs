namespace Pizzeria.Domain.Inventario;

using Pizzeria.Domain.Organizacion;

public class StockInsumo
{
    public Insumo Insumo { get; private set; }
    public double CantidadActual { get; private set; }
    public double CantidadMinima { get; private set; }
    public Franquicia Franquicia { get; private set; }

    public StockInsumo(Insumo insumo, double cantidadActual, double cantidadMinima, Franquicia franquicia)
    {
        Insumo = insumo;
        CantidadActual = cantidadActual;
        CantidadMinima = cantidadMinima;
        Franquicia = franquicia;
    }

    public bool EsBajoStock() => CantidadActual <= CantidadMinima;

    public bool Descontar(double cantidad)
    {
        if (cantidad <= 0 || CantidadActual < cantidad) return false;
        CantidadActual -= cantidad;
        return true;
    }

    public void Reabastecer(double cantidad)
    {
        if (cantidad <= 0) throw new ArgumentException("La cantidad debe ser positiva");
        CantidadActual += cantidad;
    }

    public double GetCantidadActual() => CantidadActual;
}
