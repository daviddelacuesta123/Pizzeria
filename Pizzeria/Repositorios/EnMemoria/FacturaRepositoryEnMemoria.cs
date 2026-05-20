namespace Pizzeria.Repositorios.EnMemoria;

using Pizzeria.Domain.Facturacion;
using Pizzeria.Repositorios.Interfaces;

public class FacturaRepositoryEnMemoria : IFacturaRepository
{
    private readonly List<Factura> _facturas = new();

    public void Agregar(Factura entidad)
    {
        if (entidad == null) throw new ArgumentNullException(nameof(entidad));
        _facturas.Add(entidad);
    }

    public Factura? ObtenerPorId(int id) => _facturas.FirstOrDefault(f => f.Id == id);

    public IReadOnlyList<Factura> ObtenerTodos() => _facturas.AsReadOnly();

    public Factura? ObtenerPorCufe(string cufe) => _facturas.FirstOrDefault(f => f.Cufe == cufe);
}
