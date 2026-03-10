using Pizzeria.Domain.Pedidos;

namespace Pizzeria.Servicios.Interfaces
{
    public interface IPedidoService
    {
        void CrearPedido(Pedido pedido);
        double CalcularTotal(Pedido pedido);
    }
}
