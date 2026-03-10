using Pizzeria.Domain.Clientes;

namespace Pizzeria.Domain.Pedidos
{
    public abstract class Pedido
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public List<ItemPedido> Items { get; set; } = new();
        public DateTime Fecha { get; set; } = DateTime.Now;

        public void AgregarProducto(ItemPedido item)
        {
            Items.Add(item);
        }

        public double CalcularSubtotal()
        {
            return Items.Sum(i => i.Subtotal());
        }

        public abstract double CalcularCostoAdicional();
        public abstract double CalcularCostoFinal();
    }
}
