namespace Pizzeria.Domain.Productos;

public class Entrada : Producto
{
    public Entrada(int id, string nombre, double precioBase, string descripcion = "")
    {
        Id = id;
        Nombre = nombre;
        PrecioBase = precioBase;
        Descripcion = descripcion;
    }

    public override double CalcularPrecio() => PrecioBase;
}
