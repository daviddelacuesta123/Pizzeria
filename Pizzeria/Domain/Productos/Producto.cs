namespace Pizzeria.Domain.Productos;

public abstract class Producto
{
    public int Id { get; protected set; }
    public string Nombre { get; protected set; } = "";
    public double PrecioBase { get; protected set; }
    public string Descripcion { get; protected set; } = "";

    public abstract double CalcularPrecio();
}
