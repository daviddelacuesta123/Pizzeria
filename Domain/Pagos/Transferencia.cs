namespace Pizzeria.Domain.Pagos
{
    public class Transferencia : IMedioPago
    {
        public bool ProcesarPago(double monto) => true;
    }
}
