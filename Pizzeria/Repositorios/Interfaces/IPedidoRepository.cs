namespace Pizzeria.Repositorios.Interfaces;

using Pizzeria.Domain.Pedidos;

public interface IPedidoRepository : IRepository<IPedido>
{
    IReadOnlyList<IPedido> ObtenerPorCliente(int clienteId);
}
