namespace Pizzeria.Eventos;

using Pizzeria.Domain.Pedidos;
using Pizzeria.Domain.Pagos;

public class PagoProcessadoEvento
{
    public Pedido Pedido { get; private set; }
    public IMedioPago MedioPago { get; private set; }
    public double Monto { get; private set; }

    public PagoProcessadoEvento(Pedido pedido, IMedioPago medioPago, double monto)
    {
        Pedido = pedido;
        MedioPago = medioPago;
        Monto = monto;
    }

    public Pedido GetPedido() => Pedido;
    public double GetMonto() => Monto;
}
