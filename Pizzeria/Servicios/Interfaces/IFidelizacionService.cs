namespace Pizzeria.Servicios.Interfaces;

using Pizzeria.Domain.Clientes;

public interface IFidelizacionService
{
    void AcumularPuntos(Cliente cliente, double monto);
    double AplicarDescuentoCumpleanos(Cliente cliente, double subtotal);
    double CanjearPuntos(Cliente cliente, int puntos, double precio);
}
