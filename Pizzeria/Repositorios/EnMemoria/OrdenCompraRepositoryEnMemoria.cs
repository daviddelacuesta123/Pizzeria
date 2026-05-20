namespace Pizzeria.Repositorios.EnMemoria;

using Pizzeria.Domain.Inventario;
using Pizzeria.Repositorios.Interfaces;

public class OrdenCompraRepositoryEnMemoria : IOrdenCompraRepository
{
    private readonly List<OrdenCompra> _ordenes = new();

    public void Agregar(OrdenCompra entidad)
    {
        if (entidad == null) throw new ArgumentNullException(nameof(entidad));
        _ordenes.Add(entidad);
    }

    public OrdenCompra? ObtenerPorId(int id) => _ordenes.FirstOrDefault(o => o.Id == id);

    public IReadOnlyList<OrdenCompra> ObtenerTodos() => _ordenes.AsReadOnly();

    public IReadOnlyList<OrdenCompra> ObtenerPorEstado(EstadoOrden estado)
    {
        return _ordenes.Where(o => o.Estado == estado).ToList().AsReadOnly();
    }
}
