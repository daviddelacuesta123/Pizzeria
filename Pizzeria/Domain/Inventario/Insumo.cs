namespace Pizzeria.Domain.Inventario;

public class Insumo
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public string UnidadMedida { get; private set; }
    public string Descripcion { get; private set; }

    public Insumo(int id, string nombre, string unidadMedida, string descripcion = "")
    {
        Id = id;
        Nombre = nombre;
        UnidadMedida = unidadMedida;
        Descripcion = descripcion;
    }

    public string GetNombre() => Nombre;
    public string GetUnidadMedida() => UnidadMedida;
    public override string ToString() => $"{Nombre} ({UnidadMedida})";
}
