namespace Pizzeria.Domain.Productos;

public class Postre : Producto
{
    public string TipoPostre { get; private set; }
    public int Porciones { get; private set; }

    public Postre(int id, string nombre, double precioBase, string tipoPostre, int porciones)
    {
        Id = id;
        Nombre = nombre;
        PrecioBase = precioBase;
        TipoPostre = tipoPostre;
        Porciones = porciones;
    }

    public override double CalcularPrecio() => PrecioBase;
}
