namespace Pizzeria.Servicios;

using Pizzeria.Domain.Pedidos;
using Pizzeria.Domain.Pedidos.Factories;
using Pizzeria.Domain.Clientes;
using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Pagos;
using Pizzeria.Servicios.Interfaces;
using Pizzeria.Repositorios.Interfaces;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _repo;
    private readonly IDictionary<string, IPedidoFactory> _factories;
    private int _siguienteId = 1;

    public PedidoService(IPedidoRepository repo, IEnumerable<IPedidoFactory> factories)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        if (factories == null) throw new ArgumentNullException(nameof(factories));
        _factories = factories.ToDictionary(f => f.Tipo.ToLower(), f => f);
    }

    public IPedido CrearPedido(Cliente cliente, Franquicia franquicia, string tipo, IMedioPago medioPago)
    {
        var key = tipo?.ToLower() ?? "";
        if (!_factories.TryGetValue(key, out var factory))
            throw new ArgumentException($"Tipo de pedido no soportado: {tipo}");

        var pedido = factory.Crear(_siguienteId++, cliente, franquicia, medioPago);
        _repo.Agregar(pedido);
        return pedido;
    }

    public void ConfirmarPedido(int id)
    {
        var pedido = _repo.ObtenerPorId(id);
        if (pedido == null) throw new InvalidOperationException($"Pedido {id} no encontrado");
        Console.WriteLine($"[PedidoService] Pedido #{id} confirmado. Total: ${pedido.CalcularTotalCliente():N0}");
    }

    public double CalcularTotales(IPedido pedido) => pedido.CalcularTotalCliente();

    public IReadOnlyList<IPedido> GetPedidos() => _repo.ObtenerTodos();
}
