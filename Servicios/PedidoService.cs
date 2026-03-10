using Pizzeria.Domain.Pedidos;
using Pizzeria.Servicios.Interfaces;

namespace Pizzeria.Servicios
{
    public class PedidoService : IPedidoService
    {
        public void CrearPedido(Pedido pedido)
        {
            Console.WriteLine("Pedido registrado.");
        }

        public double CalcularTotal(Pedido pedido)
        {
            return pedido.CalcularCostoFinal();
        }
    }
}
