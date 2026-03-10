namespace Pizzeria.Domain.Pedidos
{
    public class DomicilioPropio : Pedido
    {
        public string Direccion { get; set; }
        public double CostoEnvio { get; set; } = 5000;

        public override double CalcularCostoAdicional() => CostoEnvio;

        public override double CalcularCostoFinal()
        {
            return CalcularSubtotal() + CostoEnvio;
        }
    }
}
