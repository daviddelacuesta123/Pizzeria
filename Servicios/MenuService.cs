using System;
using System.Collections.Generic;
using Pizzeria.Domain.Clientes;
using Pizzeria.Domain.Pedidos;
using Pizzeria.Domain.Productos;
using Pizzeria.Domain.Productos.Decorators;

namespace Pizzeria.Servicios
{
    public class MenuService
    {
        public void MostrarEncabezado()
        {
            Console.Clear();
            Console.WriteLine("=======================================");
            Console.WriteLine("        SISTEMA DE PIZZERÍA");
            Console.WriteLine("=======================================");
            Console.WriteLine($"Fecha: {DateTime.Now:dd/MM/yyyy}");
            Console.WriteLine("=======================================\n");
        }

        public Cliente CrearCliente()
        {
            Console.WriteLine("--- REGISTRO DE CLIENTE ---");
            Console.Write("Ingrese su nombre: ");
            string nombre = Console.ReadLine() ?? "Cliente";
            
            Console.Write("Ingrese su fecha de nacimiento (AAAA-MM-DD): ");
            DateTime fechaNac;
            while (!DateTime.TryParse(Console.ReadLine(), out fechaNac))
            {
                Console.Write("Formato inválido. Ingrese nuevamente (AAAA-MM-DD): ");
            }

            Console.Write("Ingrese su teléfono: ");
            string tel = Console.ReadLine() ?? "";

            return new Cliente { Nombre = nombre, FechaNacimiento = fechaNac, Telefono = tel };
        }

        public Pizza SeleccionarYPersonalizarPizza()
        {
            Console.WriteLine("\n--- SELECCIONE SU TIPO DE PIZZA ---");
            Console.WriteLine("1. Pizza Normal");
            Console.WriteLine("2. Pizza Estofada");
            Console.WriteLine("3. Pizza Especial");
            Console.Write("Opción: ");
            string op = Console.ReadLine() ?? "1";

            PizzaBuilder builder = op switch
            {
                "2" => new PizzaEstofadaBuilder(),
                "3" => new PizzaEspecialBuilder(),
                _ => new PizzaNormalBuilder()
            };
            Chef chef = new Chef(builder);

            Console.WriteLine("\nTamaño:");
            Console.WriteLine("1. Pequeña");
            Console.WriteLine("2. Mediana");
            Console.WriteLine("3. Grande");
            Console.Write("Seleccione: ");
            string tamOp = Console.ReadLine() ?? "2";
            string tam = tamOp switch { "1" => "Pequeña", "3" => "Grande", _ => "Mediana" };

            // DETERMINAR PRECIO BASE SEGUN TIPO Y TAMAÑO
            double precioBase = 0;
            if (op == "3") // Especial
            {
                Console.WriteLine("\nSabor Especial:");
                Console.WriteLine("1. Mexicana");
                Console.WriteLine("2. 4 Quesos");
                Console.Write("Seleccione: ");
                string sabOp = Console.ReadLine() ?? "1";
                string sabor = sabOp == "2" ? "4 Quesos" : "Mexicana";
                
                // Usamos la lógica de la clase Especial para el precio base
                var tempEspecial = new Especial(tam, sabor);
                precioBase = tempEspecial.PrecioBase;
                builder.SetNombre(tempEspecial.Nombre).SetTamano(tam).SetPrecioBase(precioBase);
            }
            else if (op == "2") // Estofada
            {
                var tempEstofada = new Estofada(tam);
                precioBase = tempEstofada.PrecioBase;
                builder.SetNombre(tempEstofada.Nombre).SetTamano(tam).SetPrecioBase(precioBase);
            }
            else // Normal
            {
                Console.WriteLine("\nSabor Normal:");
                Console.WriteLine("1. Hawaiana");
                Console.WriteLine("2. Pepperoni");
                Console.Write("Seleccione: ");
                string sabOp = Console.ReadLine() ?? "1";
                string sabor = sabOp == "2" ? "Pepperoni" : "Hawaiana";

                var tempNormal = new Normal(tam, sabor);
                precioBase = tempNormal.PrecioBase;
                builder.SetNombre(tempNormal.Nombre).SetTamano(tam).SetPrecioBase(precioBase);
            }

            Pizza pizza = builder.Build();

            // AGREGAR DECORADORES INTERACTIVAMENTE
            bool agregando = true;
            while (agregando)
            {
                Console.WriteLine($"\nPizza actual: {pizza.Nombre} ({pizza.Tamano}) - Precio actual: ${pizza.CalcularPrecio()}");
                Console.WriteLine("¿Desea agregar adiciones?");
                Console.WriteLine("1. Queso Extra ($2,500)");
                Console.WriteLine("2. Tocineta ($3,000)");
                Console.WriteLine("3. Champiñones ($2,000)");
                Console.WriteLine("4. Ninguna / Terminar");
                Console.Write("Seleccione: ");
                string addOp = Console.ReadLine() ?? "4";

                switch (addOp)
                {
                    case "1": pizza = new QuesoExtra(pizza); break;
                    case "2": pizza = new Tocineta(pizza); break;
                    case "3": pizza = new Champinones(pizza); break;
                    default: agregando = false; break;
                }
            }

            return pizza;
        }

        public void AgregarProductosAdicionales(Pedido pedido)
        {
            Console.WriteLine("\n--- ¿DESEA AGREGAR ALGO MÁS? ---");
            
            // ENTRADAS
            Console.Write("¿Desea agregar una Entrada? (s/n): ");
            if (Console.ReadLine()?.ToLower() == "s")
            {
                Console.WriteLine("1. Pan de Ajo ($8,000)");
                Console.WriteLine("2. Nachos ($12,000)");
                Console.Write("Seleccione: ");
                string entOp = Console.ReadLine() ?? "1";
                string nomEnt = entOp == "2" ? "Nachos" : "Pan de Ajo";
                pedido.AgregarProducto(new ItemPedido { Producto = new Entrada(nomEnt), Cantidad = 1 });
            }

            // BEBIDAS
            Console.Write("¿Desea agregar una Bebida? (s/n): ");
            if (Console.ReadLine()?.ToLower() == "s")
            {
                Console.WriteLine("1. Gaseosa ($5,000)");
                Console.WriteLine("2. Jugo Natural ($7,000)");
                Console.WriteLine("3. Cerveza ($9,000)");
                Console.Write("Seleccione: ");
                string bebOp = Console.ReadLine() ?? "1";
                string nomBeb = bebOp switch { "2" => "Jugo Natural", "3" => "Cerveza", _ => "Gaseosa" };
                pedido.AgregarProducto(new ItemPedido { Producto = new Bebida(nomBeb), Cantidad = 1 });
            }

            // POSTRES
            Console.Write("¿Desea agregar un Postre? (s/n): ");
            if (Console.ReadLine()?.ToLower() == "s")
            {
                Console.WriteLine("1. Brownie ($8,000)");
                Console.WriteLine("2. Cheesecake ($10,000)");
                Console.Write("Seleccione: ");
                string posOp = Console.ReadLine() ?? "1";
                string nomPos = posOp == "2" ? "Cheesecake" : "Brownie";
                pedido.AgregarProducto(new ItemPedido { Producto = new Postre(nomPos), Cantidad = 1 });
            }
        }
        public Pedido IniciarPedido(Cliente cliente)
        {
            Console.WriteLine("\n--- TIPO DE ENTREGA ---");
            Console.WriteLine("1. Consumo en Local");
            Console.WriteLine("2. Domicilio propio del local");
            Console.WriteLine("3. Domicilio Rappi");
            Console.Write("Opción: ");
            string op = Console.ReadLine() ?? "1";

            Pedido pedido = op switch
            {
                "2" => new DomicilioPropio { Direccion = "Calle 123" },
                "3" => new DomicilioRappi { Direccion = "Calle Rappi 45" },
                _ => new Local()
            };

            pedido.Cliente = cliente;
            return pedido;
        }
    }
}
