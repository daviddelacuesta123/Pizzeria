namespace Pizzeria.Repositorios.EnMemoria;

using Pizzeria.Domain.Pedidos;
using Pizzeria.Repositorios.Interfaces;

public class PedidoRepositoryEnMemoria : IPedidoRepository
{
    private readonly List<IPedido> _pedidos = new();

    public void Agregar(IPedido entidad)
    {
        if (entidad == null) throw new ArgumentNullException(nameof(entidad));
        _pedidos.Add(entidad);
    }

    public IPedido? ObtenerPorId(int id) => _pedidos.FirstOrDefault(p => p.Id == id);

    public IReadOnlyList<IPedido> ObtenerTodos() => _pedidos.AsReadOnly();

    public IReadOnlyList<IPedido> ObtenerPorCliente(int clienteId)
    {
        return _pedidos
            .OfType<Pedido>()
            .Where(p => p.Cliente.Id == clienteId)
            .Cast<IPedido>()
            .ToList()
            .AsReadOnly();
    }
}
