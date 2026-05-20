namespace Pizzeria.Domain.Pedidos.Factories;

using Pizzeria.Domain.Clientes;
using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Pagos;

public interface IPedidoFactory
{
    string Tipo { get; }
    IPedido Crear(int id, Cliente cliente, Franquicia franquicia, IMedioPago medioPago);
}
