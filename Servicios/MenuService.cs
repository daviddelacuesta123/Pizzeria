using Pizzeria.Domain.Clientes;
using Pizzeria.Domain.Pedidos;
using Pizzeria.Domain.Productos;

namespace Pizzeria.Servicios
{
  
    public class MenuService
    {
        public Cliente CrearCliente()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("        REGISTRO DE CLIENTE");
            Console.WriteLine("=======================================");
            
            Console.Write("Nombre del cliente: ");
            string nombre = Console.ReadLine() ?? "";

            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine() ?? "";

            Console.Write("Fecha de nacimiento (yyyy-mm-dd): ");
            DateTime fechaNacimiento;
            
            while (!DateTime.TryParse(Console.ReadLine(), out fechaNacimiento))
            {
                Console.Write("Fecha inválida. Intente nuevamente (yyyy-mm-dd): ");
            }

            return new Cliente
            {
                Nombre = nombre,
                Telefono = telefono,
                FechaNacimiento = fechaNacimiento
            };
        }

        public Pedido CrearPedido(Cliente cliente)
        {
            Console.WriteLine("\n=======================================");
            Console.WriteLine("        TIPO DE PEDIDO");
            Console.WriteLine("=======================================");
            Console.WriteLine("1. Local");
            Console.WriteLine("2. Domicilio Propio");
            Console.WriteLine("3. Domicilio Rappi");
            Console.Write("Seleccione opción: ");

            Pedido pedido = Console.ReadLine() switch
            {
                "2" => CrearDomicilioPropio(),
                "3" => CrearDomicilioRappi(),
                _ => new Local()
            };

            pedido.Cliente = cliente;
            return pedido;
        }

        private DomicilioPropio CrearDomicilioPropio()
        {
            Console.Write("Ingrese la dirección de entrega: ");
            string direccion = Console.ReadLine() ?? "";
            
            return new DomicilioPropio { Direccion = direccion };
        }

        private DomicilioRappi CrearDomicilioRappi()
        {
            Console.Write("Ingrese la dirección de entrega: ");
            string direccion = Console.ReadLine() ?? "";
            
            return new DomicilioRappi { Direccion = direccion };
        }

        public Pizza CrearPizza()
        {
            Console.WriteLine("\n=======================================");
            Console.WriteLine("        SELECCIÓN DE PIZZA");
            Console.WriteLine("=======================================");
            Console.WriteLine("Tipo de pizza:");
            Console.WriteLine("1. Normal");
            Console.WriteLine("2. Especial");
            Console.Write("Seleccione: ");
            string tipoPizza = Console.ReadLine() ?? "1";

            Console.WriteLine("\nTamaño:");
            Console.WriteLine("1. Pequeña");
            Console.WriteLine("2. Mediana");
            Console.WriteLine("3. Grande");
            Console.Write("Seleccione: ");

            string tamano = Console.ReadLine() switch
            {
                "1" => "Pequeña",
                "3" => "Grande",
                _ => "Mediana"
            };

            string sabor = "";

            if (tipoPizza == "2")
            {
                Console.WriteLine("\nSabores disponibles (Especial):");
                Console.WriteLine("1. Mexicana");
                Console.WriteLine("2. 4 Quesos");
                Console.Write("Seleccione: ");

                sabor = Console.ReadLine() switch
                {
                    "2" => "4 Quesos",
                    _ => "Mexicana"
                };
            }
            else
            {
                Console.WriteLine("\nSabores disponibles (Normal):");
                Console.WriteLine("1. Hawaiana");
                Console.WriteLine("2. Pepperoni");
                Console.Write("Seleccione: ");

                sabor = Console.ReadLine() switch
                {
                    "2" => "Pepperoni",
                    _ => "Hawaiana"
                };
            }

            Pizza pizza = tipoPizza == "2"
                ? new Especial(tamano, sabor)
                : new Normal(tamano, sabor);

            AgregarIngredientesExtras(pizza);

            return pizza;
        }

        private void AgregarIngredientesExtras(Pizza pizza)
        {
            Console.Write("\n¿Cuántos ingredientes extra desea agregar?: ");
            int extras;
            
            while (!int.TryParse(Console.ReadLine(), out extras) || extras < 0)
            {
                Console.Write("Número inválido. Intente nuevamente: ");
            }

            for (int i = 0; i < extras; i++)
            {
                Console.Write($"Ingrediente #{i + 1}: ");
                string nombreIng = Console.ReadLine() ?? "";

                pizza.AgregarIngrediente(new Ingrediente
                {
                    Nombre = nombreIng,
                    PrecioExtra = 3000,
                    EsExtra = true
                });
            }
        }

        public void AgregarProductosAdicionales(Pedido pedido)
        {
            AgregarEntrada(pedido);
            AgregarBebida(pedido);
            AgregarPostre(pedido);
        }

        private void AgregarEntrada(Pedido pedido)
        {
            Console.WriteLine("\n¿Desea agregar entrada? (s/n)");
            if (Console.ReadLine()?.ToLower() != "s") return;

            Console.WriteLine("1. Pan de Ajo ($8,000)");
            Console.WriteLine("2. Nachos ($12,000)");
            Console.Write("Seleccione: ");

            string nombreEntrada = Console.ReadLine() switch
            {
                "2" => "Nachos",
                _ => "Pan de Ajo"
            };

            pedido.AgregarProducto(new ItemPedido
            {
                Producto = new Entrada(nombreEntrada),
                Cantidad = 1
            });
        }

        private void AgregarBebida(Pedido pedido)
        {
            Console.WriteLine("\n¿Desea agregar bebida? (s/n)");
            if (Console.ReadLine()?.ToLower() != "s") return;

            Console.WriteLine("1. Gaseosa ($5,000)");
            Console.WriteLine("2. Jugo Natural ($7,000)");
            Console.WriteLine("3. Cerveza ($9,000)");
            Console.Write("Seleccione: ");

            string nombreBebida = Console.ReadLine() switch
            {
                "2" => "Jugo Natural",
                "3" => "Cerveza",
                _ => "Gaseosa"
            };

            pedido.AgregarProducto(new ItemPedido
            {
                Producto = new Bebida(nombreBebida),
                Cantidad = 1
            });
        }

        private void AgregarPostre(Pedido pedido)
        {
            Console.WriteLine("\n¿Desea agregar postre? (s/n)");
            if (Console.ReadLine()?.ToLower() != "s") return;

            Console.WriteLine("1. Brownie ($8,000)");
            Console.WriteLine("2. Cheesecake ($10,000)");
            Console.Write("Seleccione: ");

            string nombrePostre = Console.ReadLine() switch
            {
                "2" => "Cheesecake",
                _ => "Brownie"
            };

            pedido.AgregarProducto(new ItemPedido
            {
                Producto = new Postre(nombrePostre),
                Cantidad = 1
            });
        }

        public void MostrarEncabezado()
        {
            DateTime fechaActual = DateTime.Now;
            Console.WriteLine("=======================================");
            Console.WriteLine("        SISTEMA RESTAURANTE");
            Console.WriteLine("=======================================");
            Console.WriteLine($"Fecha: {fechaActual:dd/MM/yyyy}");
            Console.WriteLine($"Hora : {fechaActual:HH:mm:ss}");
            Console.WriteLine("=======================================\n");
        }
    }
}
