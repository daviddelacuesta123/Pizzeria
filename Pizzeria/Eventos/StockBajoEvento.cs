namespace Pizzeria.Eventos;

using Pizzeria.Domain.Inventario;
using Pizzeria.Domain.Organizacion;

public class StockBajoEvento
{
    public Insumo Insumo { get; private set; }
    public double CantidadActual { get; private set; }
    public Franquicia Franquicia { get; private set; }

    public StockBajoEvento(Insumo insumo, double cantidadActual, Franquicia franquicia)
    {
        Insumo = insumo;
        CantidadActual = cantidadActual;
        Franquicia = franquicia;
    }

    public Insumo GetInsumo() => Insumo;
    public Franquicia GetFranquicia() => Franquicia;
}
