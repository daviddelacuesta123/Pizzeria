namespace Pizzeria.Domain.Productos.Estados
{
    public class PedidoRecibido : EstadoPizza
    {
        public void ManejarEstado(Pizza pizza)
        {
            pizza.CambiarEstado(new EnPreparacion());
        }

        public string GetNombre()
        {
            return "Pedido Recibido";
        }
    }
}
