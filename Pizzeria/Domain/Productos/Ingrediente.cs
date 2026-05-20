namespace Pizzeria.Domain.Productos;

using Pizzeria.Domain.Inventario;

public class Ingrediente
{
    public string Nombre { get; private set; }
    public double PrecioExtra { get; private set; }
    public bool EsExtra { get; private set; }
    public Insumo? InsumoRequerido { get; private set; }

    public Ingrediente(string nombre, double precioExtra, bool esExtra, Insumo? insumoRequerido = null)
    {
        Nombre = nombre;
        PrecioExtra = precioExtra;
        EsExtra = esExtra;
        InsumoRequerido = insumoRequerido;
    }

    public Insumo? GetInsumo() => InsumoRequerido;
}
