using System;
using Pizzeria.Domain.Pagos;
using Pizzeria.Domain.Pedidos;

namespace Pizzeria.Servicios
{

    public class PagoService
    {
        public IMedioPago SeleccionarMedioPago(Pedido pedido)
        {
            bool esRappi = pedido is DomicilioRappi;

            Console.WriteLine("\n=======================================");
            Console.WriteLine("        MEDIO DE PAGO");
            Console.WriteLine("=======================================");
            Console.WriteLine("1. Efectivo");
            Console.WriteLine("2. Tarjeta");
            Console.WriteLine("3. Transferencia");

            if (esRappi)
            {
                Console.WriteLine("4. RappiPago");
            }

            Console.Write("Seleccione: ");
            string opcion = Console.ReadLine() ?? "1";

            if (opcion == "4" && !esRappi)
            {
                Console.WriteLine("RappiPago solo disponible para pedidos de Rappi. Usando Efectivo por defecto.");
                return new Efectivo();
            }

            return opcion switch
            {
                "2" => new Tarjeta(),
                "3" => new Transferencia(),
                "4" => new RappiPago(),
                _ => new Efectivo()
            };
        }
    }
}
