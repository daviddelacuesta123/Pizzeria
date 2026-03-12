using System.Collections.Generic;

namespace Pizzeria.Domain.Productos
{
    public class PizzaEstofadaBuilder : PizzaBuilder
    {
        private Estofada _pizza;

        public PizzaEstofadaBuilder()
        {
            _pizza = new Estofada();
            _pizza.Nombre = "Pizza Estofada Personalizada";
        }

        public PizzaBuilder SetTamano(string tamano)
        {
            _pizza.Tamano = tamano;
            return this;
        }

        public PizzaBuilder SetIngredientes(List<Ingrediente> ingredientes)
        {
            _pizza.Ingredientes = ingredientes;
            return this;
        }

        public PizzaBuilder SetPrecioBase(double precio)
        {
            _pizza.PrecioBase = precio;
            return this;
        }

        public Pizza Build()
        {
            return _pizza;
        }
    }
}
