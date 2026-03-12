namespace Pizzeria.Domain.Productos.Commands
{
    public class PrepararPizzaCommand : Command
    {
        private Pizza _pizza;
        private Cocina _cocina;
        private Estados.EstadoPizza _estadoAnterior;

        public PrepararPizzaCommand(Pizza pizza, Cocina cocina)
        {
            _pizza = pizza;
            _cocina = cocina;
            _estadoAnterior = pizza.GetEstado();
        }

        public void Ejecutar()
        {
            _cocina.PrepararPizza(_pizza);
        }

        public void Deshacer()
        {
            _pizza.CambiarEstado(_estadoAnterior);
        }
    }
}
