namespace Pizzeria.Repositorios.Interfaces;

using Pizzeria.Domain.Inventario;

public interface IOrdenCompraRepository : IRepository<OrdenCompra>
{
    IReadOnlyList<OrdenCompra> ObtenerPorEstado(EstadoOrden estado);
}
