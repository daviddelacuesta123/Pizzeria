namespace Pizzeria.Domain.Inventario;

using Pizzeria.Domain.Organizacion;

public class ConvenioRappi
{
    public int Id { get; private set; }
    public Franquicia Franquicia { get; private set; }
    public double PorcentajeComision { get; private set; }
    public bool Activo { get; private set; }
    public DateTime FechaInicio { get; private set; }

    public ConvenioRappi(int id, Franquicia franquicia, double porcentajeComision, DateTime fechaInicio)
    {
        if (porcentajeComision < 0 || porcentajeComision > 1)
            throw new ArgumentException("La comisión debe estar entre 0 y 1 (ej. 0.25 = 25%)");
        Id = id;
        Franquicia = franquicia;
        PorcentajeComision = porcentajeComision;
        FechaInicio = fechaInicio;
        Activo = true;
    }

    public bool EstaActivo() => Activo;
    public double CalcularComision(double monto) => Activo ? monto * PorcentajeComision : 0;
    public void Activar() => Activo = true;
    public void Desactivar() => Activo = false;
}
