namespace Pizzeria.Domain.Pedidos;

using Pizzeria.Domain.Clientes;
using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Inventario;
using Pizzeria.Domain.Pagos;

public class Local : Pedido
{
    public Mesa Mesa { get; private set; }
    public Mesero Mesero { get; private set; }

    public Local(int id, Cliente cliente, Franquicia franquicia, IMedioPago medioPago, Mesa mesa, Mesero mesero)
        : base(id, cliente, franquicia, medioPago)
    {
        Mesa = mesa;
        Mesero = mesero;
    }

    public void AsignarMesa(Mesa mesa) => Mesa = mesa;
}
