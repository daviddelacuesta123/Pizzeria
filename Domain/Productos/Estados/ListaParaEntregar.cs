namespace Pizzeria.Domain.Productos.Estados
{
    public class ListaParaEntregar : EstadoPizza
    {
        public void ManejarEstado(Pizza pizza)
        {
            pizza.CambiarEstado(new EnCamino());
        }

        public string GetNombre()
        {
            return "Lista Para Entregar";
        }
    }
}
