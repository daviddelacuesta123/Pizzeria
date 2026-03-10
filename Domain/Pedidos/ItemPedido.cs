using Pizzeria.Domain.Productos;

namespace Pizzeria.Domain.Pedidos
{
    public class ItemPedido
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }

        public double Subtotal()
        {
            return Producto.CalcularPrecio() * Cantidad;
        }
    }
}
