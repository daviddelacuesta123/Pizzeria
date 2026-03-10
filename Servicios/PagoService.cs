using Pizzeria.Domain.Pagos;

namespace Pizzeria.Servicios
{
    
    public class PagoService
    {
        public IMedioPago SeleccionarMedioPago()
        {
            Console.WriteLine("\n=======================================");
            Console.WriteLine("        MEDIO DE PAGO");
            Console.WriteLine("=======================================");
            Console.WriteLine("1. Efectivo");
            Console.WriteLine("2. Tarjeta");
            Console.WriteLine("3. Transferencia");
            Console.WriteLine("4. RappiPago");
            Console.Write("Seleccione: ");

            return Console.ReadLine() switch
            {
                "2" => new Tarjeta(),
                "3" => new Transferencia(),
                "4" => new RappiPago(),
                _ => new Efectivo()
            };
        }
    }
}
