using System.Collections.Generic;

namespace Pizzeria.Domain.Productos
{
    public class PizzaNormalBuilder : PizzaBuilder
    {
        private Normal _pizza;

        public PizzaNormalBuilder()
        {
            _pizza = new Normal();
            _pizza.Nombre = "Pizza Normal Personalizada";
            _pizza.Sabor = "Estandar";
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
