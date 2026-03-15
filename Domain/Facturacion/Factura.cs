using Pizzeria.Domain.Pedidos;
using Pizzeria.Domain.Pagos;
using Pizzeria.Domain.Productos;

namespace Pizzeria.Domain.Facturacion
{
    public class Factura
    {
        public int NumeroFactura { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public double Subtotal { get; set; }
        public double Descuento { get; set; }
        public double CostoAdicional { get; set; }
        public double Total { get; set; }
        public Pedido Pedido { get; set; } = null!;
        public IMedioPago MedioPago { get; set; } = null!;

        public void ImprimirFactura()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("║              FACTURA DE VENTA                     ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
            Console.WriteLine($"Factura No: {NumeroFactura}");
            Console.WriteLine($"Fecha: {Fecha:dd/MM/yyyy HH:mm:ss}");
            Console.WriteLine("═══════════════════════════════════════════════════");
            
            // INFORMACIÓN DEL CLIENTE
            Console.WriteLine($"Cliente: {Pedido.Cliente.Nombre}");
            Console.WriteLine($"Teléfono: {Pedido.Cliente.Telefono}");
            
            // TIPO DE PEDIDO
            string tipoPedido = Pedido switch
            {
                DomicilioPropio => "Domicilio propio del local",
                DomicilioRappi => "Domicilio Rappi",
                Local => "Local",
                _ => "N/A"
            };
            Console.WriteLine($"Tipo de pedido: {tipoPedido}");
            Console.WriteLine("═══════════════════════════════════════════════════");
            
            // DETALLE DE PRODUCTOS
            Console.WriteLine("\nDETALLE DE PRODUCTOS:");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("{0,-3} {1,-30} {2,-8} {3,10}", "Cant", "Producto", "P.Unit", "Subtotal");
            Console.WriteLine("---------------------------------------------------");
            
            foreach (var item in Pedido.Items)
            {
                double precioUnitario = item.Producto.CalcularPrecio();
                double subtotal = item.Subtotal();
                
                Console.WriteLine("{0,-3} {1,-30} ${2,-7:N0} ${3,9:N0}", 
                    item.Cantidad, 
                    item.Producto.Nombre, 
                    precioUnitario, 
                    subtotal);
                
                // Si el producto tiene extras, mostrarlos (LSP - usa interfaz)
                if (item.Producto is IProductoConExtras productoConExtras)
                {
                    var extras = productoConExtras.ObtenerExtras();
                    foreach (var ing in extras)
                    {
                        Console.WriteLine("    + {0} (${1:N0})", ing.Nombre, ing.PrecioExtra);
                    }
                }
            }
            
            Console.WriteLine("---------------------------------------------------");
            
            // TOTALES
            Console.WriteLine($"{"Subtotal:",-40} ${Subtotal,9:N0}");
            
            if (CostoAdicional > 0)
            {
                string conceptoAdicional = Pedido switch
                {
                    DomicilioPropio => "Costo de Envío",
                    DomicilioRappi => "Comisión Rappi (15%)",
                    _ => "Cargo adicional"
                };
                Console.WriteLine($"{conceptoAdicional + ":",-40} ${CostoAdicional,9:N0}");
            }
            
            if (Descuento > 0)
            {
                Console.WriteLine($"{"Descuento cumpleaños (10%):",-40} -${Descuento,8:N0}");
            }
            
            Console.WriteLine("═══════════════════════════════════════════════════");
            Console.WriteLine($"{"TOTAL A PAGAR:",-40} ${Total,9:N0}");
            Console.WriteLine("═══════════════════════════════════════════════════");
            
            // MEDIO DE PAGO
            string nombreMedioPago = MedioPago.GetType().Name;
            Console.WriteLine($"\nMedio de pago: {nombreMedioPago}");
            
            // PUNTOS DE FIDELIZACIÓN
            if (Pedido.Cliente.Puntos > 0)
            {
                Console.WriteLine($"Puntos acumulados: {Pedido.Cliente.Puntos}");
            }
            
            Console.WriteLine("\n        ¡Gracias por su compra!");
            Console.WriteLine("═══════════════════════════════════════════════════\n");
        }
    }
}
