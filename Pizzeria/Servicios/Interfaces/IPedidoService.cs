namespace Pizzeria.Servicios.Interfaces;

using Pizzeria.Domain.Pedidos;
using Pizzeria.Domain.Clientes;
using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Pagos;

public interface IPedidoService
{
    IPedido CrearPedido(Cliente cliente, Franquicia franquicia, string tipo, IMedioPago medioPago);
    void ConfirmarPedido(int id);
    double CalcularTotales(IPedido pedido);
}
