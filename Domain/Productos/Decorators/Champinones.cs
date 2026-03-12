namespace Pizzeria.Domain.Productos.Decorators
{
    public class Champinones : PizzaDecorator
    {
        private double _precioChampinones = 2000;

        public Champinones(Pizza pizza) : base(pizza)
        {
        }

        public override double CalcularPrecio()
        {
            return base.CalcularPrecio() + _precioChampinones;
        }
    }
}
