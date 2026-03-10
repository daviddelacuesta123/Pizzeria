namespace Pizzeria.Domain.Pagos
{
    public class Efectivo : IMedioPago
    {
        public bool ProcesarPago(double monto) => true;
    }
}
