namespace Pizzeria.Domain.Inventario;

public class PrecioMayorista
{
    public Insumo Insumo { get; private set; }
    public Distribuidor Distribuidor { get; private set; }
    public double PrecioUnitario { get; private set; }
    public double CantidadMinimaPedido { get; private set; }
    public DateTime FechaVigencia { get; private set; }

    public PrecioMayorista(Insumo insumo, Distribuidor distribuidor, double precioUnitario,
                           double cantidadMinimaPedido, DateTime fechaVigencia)
    {
        Insumo = insumo;
        Distribuidor = distribuidor;
        PrecioUnitario = precioUnitario;
        CantidadMinimaPedido = cantidadMinimaPedido;
        FechaVigencia = fechaVigencia;
    }

    public double GetPrecio() => PrecioUnitario;
    public bool EstaVigente() => DateTime.Now <= FechaVigencia;
}
