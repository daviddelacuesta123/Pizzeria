namespace Pizzeria.Domain.Pagos
{
    public interface IMedioPago
    {
        bool ProcesarPago(double monto);
    }
}
