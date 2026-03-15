using System.Collections.Generic;

namespace Pizzeria.Domain.Productos
{
    public class PizzaEspecialBuilder : PizzaBuilder
    {
        private Especial _pizza;

        public PizzaEspecialBuilder()
        {
            _pizza = new Especial();
            _pizza.Nombre = "Pizza Especial Personalizada";
            _pizza.Sabor = "Especial";
        }

        public PizzaBuilder SetNombre(string nombre)
        {
            _pizza.Nombre = nombre;
            return this;
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
