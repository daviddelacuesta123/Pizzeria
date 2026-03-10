namespace Pizzeria.Domain.Pedidos
{
    public class Local : Pedido
    {
        public override double CalcularCostoAdicional() => 0;

        public override double CalcularCostoFinal() => CalcularSubtotal();
    }
}
