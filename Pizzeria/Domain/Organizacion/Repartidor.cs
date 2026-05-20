namespace Pizzeria.Domain.Organizacion;

public class Repartidor : Empleado
{
    public string Vehiculo { get; private set; }
    public bool Disponible { get; private set; }

    public Repartidor(int id, string nombre, string cedula, string telefono, double salario,
                      Franquicia franquicia, string vehiculo)
        : base(id, nombre, cedula, telefono, salario, franquicia)
    {
        Vehiculo = vehiculo;
        Disponible = true;
    }

    public override string GetRol() => "Repartidor";
    public bool EstaDisponible() => Disponible;
    public void MarcarOcupado() => Disponible = false;
    public void MarcarDisponible() => Disponible = true;
}
