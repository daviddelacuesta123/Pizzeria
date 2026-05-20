namespace Pizzeria.Domain.Pagos;

public class Efectivo : IMedioPago
{
    public bool ProcesarPago(double monto)
    {
        if (monto <= 0) return false;
        Console.WriteLine($"[Pago Efectivo] Procesado: ${monto:N0}");
        return true;
    }

    public string GetNombre() => "Efectivo";
}
