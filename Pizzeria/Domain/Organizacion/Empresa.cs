namespace Pizzeria.Domain.Organizacion;

using Pizzeria.Domain.Facturacion;
using Pizzeria.Domain.ValueObjects;

public class Empresa : Pizzeria.Domain.Shared.IAggregateRoot
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public Nit Nit { get; private set; }
    public Direccion DireccionMatriz { get; private set; }
    public Telefono Telefono { get; private set; }

    private readonly List<Franquicia> _franquicias = new();
    private readonly List<ResolucionDIAN> _resoluciones = new();

    public Empresa(int id, string nombre, Nit nit, Direccion direccionMatriz, Telefono telefono)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la empresa es obligatorio");
        Id = id;
        Nombre = nombre;
        Nit = nit ?? throw new ArgumentNullException(nameof(nit));
        DireccionMatriz = direccionMatriz ?? throw new ArgumentNullException(nameof(direccionMatriz));
        Telefono = telefono ?? throw new ArgumentNullException(nameof(telefono));
    }

    public IReadOnlyList<Franquicia> GetFranquicias() => _franquicias.AsReadOnly();

    public void AgregarFranquicia(Franquicia f)
    {
        if (f == null) throw new ArgumentNullException(nameof(f));
        _franquicias.Add(f);
    }

    public IReadOnlyList<ResolucionDIAN> GetResoluciones() => _resoluciones.AsReadOnly();

    public void AgregarResolucion(ResolucionDIAN resolucion)
    {
        if (resolucion == null) throw new ArgumentNullException(nameof(resolucion));
        _resoluciones.Add(resolucion);
    }

    public ResolucionDIAN? GetResolucionVigente()
    {
        return _resoluciones.FirstOrDefault(r => r.EstaVigente());
    }

    public ReporteConsolidado GetReporteConsolidado(string periodo)
    {
        var reportes = _franquicias
            .Select(f => new ReporteFranquicia(f, periodo))
            .ToList();
        return new ReporteConsolidado(this, periodo, reportes);
    }
}
