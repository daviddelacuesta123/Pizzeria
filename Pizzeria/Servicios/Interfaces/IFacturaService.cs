namespace Pizzeria.Servicios.Interfaces;

using Pizzeria.Domain.Facturacion;
using Pizzeria.Domain.Pedidos;
using Pizzeria.Domain.Clientes;

public interface IFacturaService
{
    Factura GenerarFactura(IPedido pedido, ResolucionDIAN resolucion, string nitEmpresa,
                           Cliente cliente, string nitCliente);
    bool ValidarFacturaDIAN(Factura factura);
}
