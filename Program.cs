using System;
using System.Collections.Generic;
using Pizzeria.Domain.Clientes;
using Pizzeria.Domain.Productos;
using Pizzeria.Domain.Productos.Commands;
using Pizzeria.Domain.Productos.Decorators;
using Pizzeria.Domain.Pedidos;
using Pizzeria.Domain.Pedidos.Mementos;
using Pizzeria.Domain.Pagos;
using Pizzeria.Servicios;
using Pizzeria.Servicios.Interfaces;

namespace Pizzeria
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new MenuService();
            var facturacion = new FacturaService(new FidelizacionService());
            var pagoService = new PagoService();
            
            menu.MostrarEncabezado();

            // 1. Registro interactivo de cliente
            Cliente cliente = menu.CrearCliente();
            
            // 2. Seleccionar y personalizar pizza (Builder + Decorator)
            Pedido pedido = new Local { Cliente = cliente };
            bool ordenarMas = true;
            while (ordenarMas)
            {
                Pizza pizza = menu.SeleccionarYPersonalizarPizza();
                pedido.AgregarProducto(new ItemPedido { Producto = pizza, Cantidad = 1 });
                
                Console.Write("\n¿Desea agregar otra pizza? (s/n): ");
                ordenarMas = Console.ReadLine()?.ToLower() == "s";
            }

            // 3. Agregar otros productos (NUEVO)
            menu.AgregarProductosAdicionales(pedido);

            // 4. Seleccionar tipo de entrega (REORDENADO)
            Pedido pedidoFinal = menu.IniciarPedido(cliente);
            // Transferir items del pedido temporal al final
            foreach(var item in pedido.Items) {
                pedidoFinal.AgregarProducto(item);
            }
            pedido = pedidoFinal;
            
            // 5. Notificadores (Observer)
            pedido.Suscribir(new NotificadorCliente("SMS"));
            pedido.Suscribir(new NotificadorCumpleanos());

            // 6. Gestión de cocina (Command + State)
            Console.WriteLine("\n--- PROCESANDO PEDIDO EN COCINA ---");
            Cocina cocina = new Cocina();
            
            foreach(var item in pedido.Items)
            {
                if (item.Producto is Pizza p)
                {
                    Command cmd = new PrepararPizzaCommand(p, cocina);
                    cocina.EjecutarComando(cmd);
                    pedido.Notificar(); // Notifica el cambio a "En Preparación"
                }
            }

            // 7. Confirmación Final (Memento if needed, but here simple flow)
            Console.WriteLine("\n--- PAGO Y FACTURACIÓN ---");
            IMedioPago medioPago = pagoService.SeleccionarMedioPago();
            
            var factura = facturacion.GenerarFactura(pedido, medioPago);
            
            // Imprimir Factura Final Detallada
            factura.ImprimirFactura();

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
