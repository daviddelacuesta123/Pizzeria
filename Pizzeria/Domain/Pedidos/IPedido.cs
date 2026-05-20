namespace Pizzeria.Domain.Pedidos;

using Pizzeria.Domain.Productos;

public interface IPedido
{
    int Id { get; }
    void AgregarItem(Producto p, int cantidad);
    IReadOnlyList<ItemPedido> GetItems();
    double CalcularTotalCliente();
    double CalcularCostoParaPizzeria();
}
