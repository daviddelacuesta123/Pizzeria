namespace Pizzeria.Domain.Productos.Decorators
{
    public class QuesoExtra : PizzaDecorator
    {
        private double _precioQueso = 2500;

        public QuesoExtra(Pizza pizza) : base(pizza)
        {
        }

        public override double CalcularPrecio()
        {
            return base.CalcularPrecio() + _precioQueso;
        }
    }
}
