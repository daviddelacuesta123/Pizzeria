namespace Pizzeria.Domain.Pagos
{
    public class Tarjeta : IMedioPago
    {
        public bool ProcesarPago(double monto) => true;
    }
}
