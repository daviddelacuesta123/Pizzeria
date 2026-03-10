namespace Pizzeria.Domain.Pedidos
{
    public class DomicilioRappi : Pedido
    {
        public string Direccion { get; set; }
        public double Comision { get; set; } = 0.15;

        public override double CalcularCostoAdicional()
        {
            return CalcularSubtotal() * Comision;
        }

        public override double CalcularCostoFinal()
        {
            return CalcularSubtotal() + CalcularCostoAdicional();
        }
    }
}
