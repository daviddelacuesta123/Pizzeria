namespace Pizzeria.Domain.Pedidos;

using Pizzeria.Domain.Productos;

public class ItemPedido
{
    public Producto Producto { get; private set; }
    public int Cantidad { get; private set; }
    public double Subtotal { get; private set; }

    public ItemPedido(Producto producto, int cantidad)
    {
        if (cantidad <= 0) throw new ArgumentException("La cantidad debe ser positiva");
        Producto = producto;
        Cantidad = cantidad;
        Subtotal = producto.CalcularPrecio() * cantidad;
    }
}
