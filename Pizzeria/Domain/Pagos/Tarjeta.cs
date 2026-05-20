namespace Pizzeria.Domain.Pagos;

public class Tarjeta : IMedioPago
{
    public bool ProcesarPago(double monto)
    {
        if (monto <= 0) return false;
        Console.WriteLine($"[Pago Tarjeta] Procesado: ${monto:N0}");
        return true;
    }

    public string GetNombre() => "Tarjeta";
}
