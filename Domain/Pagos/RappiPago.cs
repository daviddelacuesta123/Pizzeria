namespace Pizzeria.Domain.Pagos
{
    public class RappiPago : IMedioPago
    {
        public bool ProcesarPago(double monto) => true;
    }
}
