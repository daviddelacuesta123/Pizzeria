namespace Pizzeria.Domain.Pagos;

public class Transferencia : IMedioPago
{
    public bool ProcesarPago(double monto)
    {
        if (monto <= 0) return false;
        Console.WriteLine($"[Pago Transferencia] Procesado: ${monto:N0}");
        return true;
    }

    public string GetNombre() => "Transferencia";
}
