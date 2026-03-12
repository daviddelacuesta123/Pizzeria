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
            Console.WriteLine("=======================================");
            Console.WriteLine("    PIZZERIA - DEMOSTRACIÓN DE PATRONES");
            Console.WriteLine("=======================================\n");

            // 1. PATRÓN BUILDER & PRODUCTOS
            Console.WriteLine("--- 1. PATRÓN BUILDER ---");
            var normalBuilder = new PizzaNormalBuilder();
            var chef = new Chef(normalBuilder);
            
            Pizza pizzaNormal = chef.ConstruirPizzaEstandar("Grande");
            Console.WriteLine($"Chef construyó: {pizzaNormal.Nombre} ({pizzaNormal.Tamano})");

            var estofadaBuilder = new PizzaEstofadaBuilder();
            chef.SetBuilder(estofadaBuilder);
            Pizza pizzaEstofada = chef.ConstruirPizzaPersonalizada("Mediana", new List<Ingrediente> { 
                new Ingrediente { Nombre = "Pepperoni", PrecioExtra = 3000, EsExtra = true } 
            });
            Console.WriteLine($"Chef construyó: {pizzaEstofada.Nombre} ({pizzaEstofada.Tamano})\n");

            // 2. PATRÓN DECORATOR
            Console.WriteLine("--- 2. PATRÓN DECORATOR ---");
            Console.WriteLine($"Precio base pizza normal: ${pizzaNormal.CalcularPrecio()}");
            
            // Decorar la pizza con ingredientes adicionales
            pizzaNormal = new Pizzeria.Domain.Productos.Decorators.QuesoExtra(pizzaNormal);
            pizzaNormal = new Pizzeria.Domain.Productos.Decorators.Tocineta(pizzaNormal);
            pizzaNormal = new Pizzeria.Domain.Productos.Decorators.Champinones(pizzaNormal);
            
            Console.WriteLine($"Precio pizza normal decorada (Queso, Tocineta, Champiñones): ${pizzaNormal.CalcularPrecio()}\n");

            // 3. PATRÓN OBSERVER
            Console.WriteLine("--- 3. PATRÓN OBSERVER ---");
            Cliente cliente = new Cliente { Nombre = "Juan Perez", FechaNacimiento = DateTime.Now }; // Hoy es su cumple
            Pedido pedido = new Local { Cliente = cliente };
            pedido.AgregarProducto(new ItemPedido { Producto = pizzaNormal, Cantidad = 1 });

            var notificador = new NotificadorCliente("WhatsApp");
            var notificadorCumple = new NotificadorCumpleanos();

            pedido.Suscribir(notificador);
            pedido.Suscribir(notificadorCumple);
            
            Console.WriteLine("Notificando cambio de estado inicial:");
            pedido.Notificar(); // Debería disparar ambos notificadores
            Console.WriteLine();

            // 4. PATRON STATE
            Console.WriteLine("--- 4. PATRÓN STATE ---");
            Console.WriteLine($"Estado actual de la pizza: {pizzaNormal.GetEstado().GetNombre()}");
            pizzaNormal.ManejarEstado(); // Cambia a En Preparación
            Console.WriteLine($"Nuevo estado (tras ManejarEstado): {pizzaNormal.GetEstado().GetNombre()}");
            pedido.Notificar(); // Notificar el nuevo estado
            Console.WriteLine();

            // 5. PATRÓN COMMAND
            Console.WriteLine("--- 5. PATRÓN COMMAND ---");
            Cocina cocina = new Cocina();
            Command prepararCmd = new PrepararPizzaCommand(pizzaNormal, cocina);
            
            Console.WriteLine("Ejecutando comando PrepararPizza...");
            cocina.EjecutarComando(prepararCmd);
            Console.WriteLine($"Estado tras comando: {pizzaNormal.GetEstado().GetNombre()}");
            
            Console.WriteLine("Deshaciendo el último comando...");
            cocina.DeshacerUltimo();
            Console.WriteLine($"Estado tras deshacer: {pizzaNormal.GetEstado().GetNombre()}\n");

            // 6. PATRÓN MEMENTO
            Console.WriteLine("--- 6. PATRÓN MEMENTO ---");
            HistorialPedidos historial = new HistorialPedidos();
            
            Console.WriteLine("Guardando el estado actual del pedido en el historial...");
            historial.Guardar(pedido.GuardarMemento());
            
            // Cambiar algo en el pedido
            pizzaNormal.CambiarEstado(new Pizzeria.Domain.Productos.Estados.EnCamino());
            Console.WriteLine($"Estado actual cambiado a: {pizzaNormal.GetEstado().GetNombre()}");
            
            Console.WriteLine("Restaurando el pedido desde el historial...");
            var memento = historial.Restaurar(0);
            if (memento != null)
            {
                Console.WriteLine($"Estado restaurado: {memento.GetEstado()}");
                Console.WriteLine($"Fecha del snapshot: {memento.GetFecha()}");
                Console.WriteLine($"Total en el snapshot: ${memento.GetTotalCliente()}");
            }

            Console.WriteLine("\n=======================================");
            Console.WriteLine("    DEMOSTRACIÓN FINALIZADA");
            Console.WriteLine("=======================================");
            
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
