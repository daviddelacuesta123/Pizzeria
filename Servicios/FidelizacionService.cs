using Pizzeria.Domain.Clientes;
using Pizzeria.Servicios.Interfaces;

namespace Pizzeria.Servicios
{
    public class FidelizacionService : IFidelizacionService
    {
        public void AcumularPuntos(Cliente cliente, double monto)
        {
            cliente.AcumularPuntos((int)(monto / 1000));
        }

        public double AplicarDescuento(Cliente cliente, double total)
        {
            if (cliente.EsCumpleanosHoy())
                return total * 0.9;

            return total;
        }
    }
}
