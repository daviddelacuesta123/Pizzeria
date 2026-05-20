namespace Pizzeria.Domain.Organizacion;

public abstract class Empleado
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public string Cedula { get; private set; }
    public string Telefono { get; private set; }
    public double Salario { get; private set; }
    public Franquicia Franquicia { get; private set; }

    protected Empleado(int id, string nombre, string cedula, string telefono, double salario, Franquicia franquicia)
    {
        Id = id;
        Nombre = nombre;
        Cedula = cedula;
        Telefono = telefono;
        Salario = salario;
        Franquicia = franquicia;
    }

    public abstract string GetRol();
    public Franquicia GetFranquicia() => Franquicia;
    public override string ToString() => $"{GetRol()} {Nombre} (CC {Cedula})";
}
