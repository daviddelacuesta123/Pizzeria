namespace Pizzeria.Domain.Pagos;

public class RappiPago : IMedioPago
{
    public bool ProcesarPago(double monto)
    {
        if (monto <= 0) return false;
        Console.WriteLine($"[Pago Rappi] Procesado: ${monto:N0}");
        return true;
    }

    public string GetNombre() => "RappiPago";
}
