using Pizzeria.Domain.Facturacion;
using Pizzeria.Domain.Pagos;
using Pizzeria.Domain.Pedidos;

namespace Pizzeria.Servicios.Interfaces
{
    public interface IFacturaService
    {
        Factura GenerarFactura(Pedido pedido, IMedioPago medioPago);
    }
}
