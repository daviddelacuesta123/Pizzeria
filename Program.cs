using Pizzeria.Domain.Clientes;
using Pizzeria.Domain.Productos;
using Pizzeria.Domain.Pedidos;
using Pizzeria.Domain.Pagos;
using Pizzeria.Servicios;
using Pizzeria.Servicios.Interfaces;

namespace Pizzeria
{
    class Program
    {
        static void Main(string[] args)
        {
            // ===============================
            // INICIALIZACIÓN DE SERVICIOS (DIP - Dependency Inversion)
            // ===============================
            var menuService = new MenuService();
            var pagoService = new PagoService();
            IFidelizacionService fidelizacionService = new FidelizacionService();
            IPedidoService pedidoService = new PedidoService();
            IFacturaService facturaService = new FacturaService(fidelizacionService);

            // ===============================
            // FLUJO PRINCIPAL (SRP - Single Responsibility)
            // ===============================
            menuService.MostrarEncabezado();
            
            // Crear cliente
            Cliente cliente = menuService.CrearCliente();
            
            // Crear pedido
            Pedido pedido = menuService.CrearPedido(cliente);
            
            // Agregar pizza
            Pizza pizza = menuService.CrearPizza();
            pedido.AgregarProducto(new ItemPedido
            {
                Producto = pizza,
                Cantidad = 1
            });
            
            // Agregar productos adicionales
            menuService.AgregarProductosAdicionales(pedido);
            
            // Seleccionar medio de pago
            IMedioPago medioPago = pagoService.SeleccionarMedioPago();

            var factura = facturaService.GenerarFactura(pedido, medioPago);

            // ===============================
            // FACTURA FINAL
            // ===============================
            factura.ImprimirFactura();

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
