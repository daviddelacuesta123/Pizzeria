namespace Pizzeria.Domain.Inventario;

public class ItemOrden
{
    public Insumo Insumo { get; private set; }
    public double Cantidad { get; private set; }
    public double PrecioUnitario { get; private set; }
    public double Subtotal { get; private set; }

    public ItemOrden(Insumo insumo, double cantidad, double precioUnitario)
    {
        Insumo = insumo;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
        Subtotal = CalcularSubtotal();
    }

    public double CalcularSubtotal() => Cantidad * PrecioUnitario;
}
