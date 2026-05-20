namespace Pizzeria.Servicios;

using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Facturacion;
using Pizzeria.Repositorios.Interfaces;

public class FranquiciaService
{
    private readonly IFranquiciaRepository _repo;
    private int _siguienteId = 1;

    public FranquiciaService(IFranquiciaRepository repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public Franquicia CrearFranquicia(Empresa empresa, string nombre, string direccion,
                                      string ciudad, string telefono)
    {
        if (empresa == null) throw new ArgumentNullException(nameof(empresa));
        var franquicia = new Franquicia(_siguienteId++, nombre, direccion, ciudad, telefono, empresa);
        empresa.AgregarFranquicia(franquicia);
        _repo.Agregar(franquicia);
        return franquicia;
    }

    public void AsignarEmpleado(Franquicia f, Empleado e)
    {
        if (f == null) throw new ArgumentNullException(nameof(f));
        if (e == null) throw new ArgumentNullException(nameof(e));
        f.AgregarEmpleado(e);
    }

    public ReporteFranquicia GetReporte(Franquicia f, string periodo) =>
        new ReporteFranquicia(f, periodo);

    public ReporteConsolidado GetReporteConsolidado(Empresa empresa, string periodo) =>
        empresa.GetReporteConsolidado(periodo);
}
