namespace Pizzeria.Repositorios.Interfaces;

using Pizzeria.Domain.Facturacion;

public interface IFacturaRepository : IRepository<Factura>
{
    Factura? ObtenerPorCufe(string cufe);
}
