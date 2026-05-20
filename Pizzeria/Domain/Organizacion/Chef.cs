namespace Pizzeria.Domain.Organizacion;

using Pizzeria.Domain.Productos;

public class Chef : Empleado
{
    public string Especialidad { get; private set; }

    public Chef(int id, string nombre, string cedula, string telefono, double salario,
                Franquicia franquicia, string especialidad)
        : base(id, nombre, cedula, telefono, salario, franquicia)
    {
        Especialidad = especialidad;
    }

    public override string GetRol() => "Chef";

    public void PrepararPizza(Pizza pizza)
    {
        if (pizza == null) throw new ArgumentNullException(nameof(pizza));
        Console.WriteLine($"[Chef {Nombre}] Preparando pizza con especialidad: {Especialidad}");
    }
}
