using Pizzeria.Domain.Facturacion;
using Pizzeria.Domain.Pagos;
using Pizzeria.Domain.Pedidos;
using Pizzeria.Servicios.Interfaces;

namespace Pizzeria.Servicios
{
    public class FacturaService : IFacturaService
    {
        private readonly IFidelizacionService _fidelizacionService;
        private static int _contadorFacturas = 1000;

        public FacturaService(IFidelizacionService fidelizacionService)
        {
            _fidelizacionService = fidelizacionService;
        }

        public Factura GenerarFactura(Pedido pedido, IMedioPago medioPago)
        {
            double subtotal = pedido.CalcularSubtotal();
            double costoAdicional = pedido.CalcularCostoAdicional();
            double totalSinDescuento = subtotal + costoAdicional;
            
            double totalConDescuento = _fidelizacionService.AplicarDescuento(pedido.Cliente, totalSinDescuento);
            double descuento = totalSinDescuento - totalConDescuento;

            if (!medioPago.ProcesarPago(totalConDescuento))
                throw new Exception("Pago rechazado");

            _fidelizacionService.AcumularPuntos(pedido.Cliente, totalConDescuento);

            return new Factura
            {
                NumeroFactura = _contadorFacturas++,
                Subtotal = subtotal,
                CostoAdicional = costoAdicional,
                Descuento = descuento,
                Total = totalConDescuento,
                Pedido = pedido,
                MedioPago = medioPago
            };
        }
    }
}
