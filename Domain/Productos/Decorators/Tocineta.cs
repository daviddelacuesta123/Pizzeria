namespace Pizzeria.Domain.Productos.Decorators
{
    public class Tocineta : PizzaDecorator
    {
        private double _precioTocineta = 3000;

        public Tocineta(Pizza pizza) : base(pizza)
        {
        }

        public override double CalcularPrecio()
        {
            return base.CalcularPrecio() + _precioTocineta;
        }
    }
}
