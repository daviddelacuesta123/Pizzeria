namespace Pizzeria.Domain.Productos;

public class Bebida : Producto
{
    public double VolumenMl { get; private set; }
    public bool Alcoholica { get; private set; }

    public Bebida(int id, string nombre, double precioBase, double volumenMl, bool alcoholica = false)
    {
        Id = id;
        Nombre = nombre;
        PrecioBase = precioBase;
        VolumenMl = volumenMl;
        Alcoholica = alcoholica;
    }

    public override double CalcularPrecio() => PrecioBase;
    public bool EsAlcoholica() => Alcoholica;
}
