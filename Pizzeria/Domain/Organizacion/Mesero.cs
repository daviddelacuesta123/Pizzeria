namespace Pizzeria.Domain.Organizacion;

using Pizzeria.Domain.Inventario;
using Pizzeria.Domain.Clientes;

public class Mesero : Empleado
{
    public string Turno { get; private set; }

    public Mesero(int id, string nombre, string cedula, string telefono, double salario,
                  Franquicia franquicia, string turno)
        : base(id, nombre, cedula, telefono, salario, franquicia)
    {
        Turno = turno;
    }

    public override string GetRol() => "Mesero";

    public void AsignarMesa(Mesa mesa)
    {
        if (mesa == null) throw new ArgumentNullException(nameof(mesa));
        if (!mesa.EstaDisponible()) throw new InvalidOperationException($"La mesa {mesa.Numero} no está disponible");
        mesa.Ocupar();
        Console.WriteLine($"[Mesero {Nombre}] Mesa {mesa.Numero} asignada");
    }

    public void TomarPedido(Cliente cliente)
    {
        if (cliente == null) throw new ArgumentNullException(nameof(cliente));
        Console.WriteLine($"[Mesero {Nombre}] Tomando pedido de {cliente.Nombre}");
    }
}
