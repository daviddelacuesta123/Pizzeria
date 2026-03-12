namespace Pizzeria.Domain.Productos.Estados
{
    public class EnHorno : EstadoPizza
    {
        public void ManejarEstado(Pizza pizza)
        {
            pizza.CambiarEstado(new ListaParaEntregar());
        }

        public string GetNombre()
        {
            return "En Horno";
        }
    }
}
