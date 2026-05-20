namespace Pizzeria.Servicios;

using Pizzeria.Domain.Clientes;
using Pizzeria.Servicios.Interfaces;

public class FidelizacionService : IFidelizacionService
{
    private const double PESO_POR_PUNTO = 1000;
    private const double DESCUENTO_CUMPLEANOS = 0.10;

    public void AcumularPuntos(Cliente cliente, double monto)
    {
        if (cliente == null) throw new ArgumentNullException(nameof(cliente));
        int puntos = (int)(monto / PESO_POR_PUNTO);
        cliente.AcumularPuntos(puntos);
        Console.WriteLine($"[Fidelización] {cliente.Nombre} acumuló {puntos} puntos (total: {cliente.PuntosAcumulados})");
    }

    public double AplicarDescuentoCumpleanos(Cliente cliente, double subtotal)
    {
        if (!cliente.EsCumpleanosHoy()) return subtotal;
        double descuento = subtotal * DESCUENTO_CUMPLEANOS;
        Console.WriteLine($"[Fidelización] 🎂 Descuento cumpleaños aplicado: ${descuento:N0}");
        return subtotal - descuento;
    }

    public double CanjearPuntos(Cliente cliente, int puntos, double precio)
    {
        if (!cliente.CanjearPuntos(puntos))
        {
            Console.WriteLine($"[Fidelización] Puntos insuficientes (tiene {cliente.PuntosAcumulados}, necesita {puntos})");
            return precio;
        }
        double descuento = puntos * 100;
        Console.WriteLine($"[Fidelización] Canjeados {puntos} puntos → descuento ${descuento:N0}");
        return Math.Max(0, precio - descuento);
    }
}
