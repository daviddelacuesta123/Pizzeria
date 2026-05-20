namespace Pizzeria.Domain.Clientes;

using Pizzeria.Domain.ValueObjects;

public class Cliente
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public Telefono Telefono { get; private set; }
    public string Email { get; private set; }
    public DateTime FechaNacimiento { get; private set; }
    public int PuntosAcumulados { get; private set; }

    public Cliente(int id, string nombre, Telefono telefono, string email, DateTime fechaNacimiento)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del cliente es obligatorio");
        Id = id;
        Nombre = nombre;
        Telefono = telefono ?? throw new ArgumentNullException(nameof(telefono));
        Email = email ?? "";
        FechaNacimiento = fechaNacimiento;
        PuntosAcumulados = 0;
    }

    public void AcumularPuntos(int puntos)
    {
        if (puntos < 0) throw new ArgumentException("Los puntos no pueden ser negativos");
        PuntosAcumulados += puntos;
    }

    public bool CanjearPuntos(int requeridos)
    {
        if (requeridos <= 0 || PuntosAcumulados < requeridos) return false;
        PuntosAcumulados -= requeridos;
        return true;
    }

    public bool EsCumpleanosHoy()
    {
        var hoy = DateTime.Now;
        return FechaNacimiento.Month == hoy.Month && FechaNacimiento.Day == hoy.Day;
    }
}
