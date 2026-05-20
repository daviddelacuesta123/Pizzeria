namespace Pizzeria.Domain.Facturacion;

public class ResolucionDIAN
{
    public string Numero { get; private set; }
    public string Prefijo { get; private set; }
    public int RangoInicio { get; private set; }
    public int RangoFin { get; private set; }
    public DateTime FechaVigencia { get; private set; }
    public int ConsecutivoActual { get; private set; }

    public ResolucionDIAN(string numero, string prefijo, int rangoInicio, int rangoFin, DateTime fechaVigencia)
    {
        Numero = numero;
        Prefijo = prefijo;
        RangoInicio = rangoInicio;
        RangoFin = rangoFin;
        FechaVigencia = fechaVigencia;
        ConsecutivoActual = rangoInicio - 1;
    }

    public int GetSiguienteConsecutivo()
    {
        if (!EstaVigente())
            throw new InvalidOperationException("La resolución DIAN no está vigente");
        if (ConsecutivoActual >= RangoFin)
            throw new InvalidOperationException("Se agotó el rango de la resolución DIAN");
        ConsecutivoActual++;
        return ConsecutivoActual;
    }

    public bool EstaVigente() => DateTime.Now <= FechaVigencia && ConsecutivoActual < RangoFin;
    public string GetNumeroCompleto() => $"{Prefijo}{ConsecutivoActual}";
}
