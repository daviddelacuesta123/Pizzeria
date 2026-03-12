namespace Pizzeria.Domain.Productos.Decorators
{
    public abstract class PizzaDecorator : Pizza
    {
        protected Pizza _pizzaDecorada;

        public PizzaDecorator(Pizza pizza)
        {
            _pizzaDecorada = pizza;
            // Copiar los atributos principales de la pizza base
            this.Tamano = pizza.Tamano;
            this.Ingredientes = pizza.Ingredientes;
        }

        public override double CalcularPrecio()
        {
            return _pizzaDecorada.CalcularPrecio();
        }

        public new void AgregarIngrediente(Ingrediente ingrediente)
        {
            _pizzaDecorada.AgregarIngrediente(ingrediente);
        }
    }
}
