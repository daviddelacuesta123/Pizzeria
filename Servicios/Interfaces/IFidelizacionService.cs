using Pizzeria.Domain.Clientes;

namespace Pizzeria.Servicios.Interfaces
{
    public interface IFidelizacionService
    {
        void AcumularPuntos(Cliente cliente, double monto);
        double AplicarDescuento(Cliente cliente, double total);
    }
}
