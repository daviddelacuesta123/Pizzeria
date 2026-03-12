namespace Pizzeria.Domain.Productos.Estados
{
    public class EnPreparacion : EstadoPizza
    {
        public void ManejarEstado(Pizza pizza)
        {
            pizza.CambiarEstado(new EnHorno());
        }

        public string GetNombre()
        {
            return "En Preparacion";
        }
    }
}
