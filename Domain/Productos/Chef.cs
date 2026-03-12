using System.Collections.Generic;

namespace Pizzeria.Domain.Productos
{
    public class Chef
    {
        private PizzaBuilder _builder;

        public Chef(PizzaBuilder builder)
        {
            _builder = builder;
        }

        public void SetBuilder(PizzaBuilder builder)
        {
            _builder = builder;
        }

        public Pizza ConstruirPizzaEstandar(string tamano)
        {
            return _builder
                .SetTamano(tamano)
                .SetPrecioBase(20000)
                .Build();
        }

        public Pizza ConstruirPizzaPersonalizada(string tamano, List<Ingrediente> ingredientes)
        {
            return _builder
                .SetTamano(tamano)
                .SetIngredientes(ingredientes)
                .SetPrecioBase(20000)
                .Build();
        }
    }
}
