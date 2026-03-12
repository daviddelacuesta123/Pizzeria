namespace Pizzeria.Domain.Productos.Estados
{
    public class EnCamino : EstadoPizza
    {
        public void ManejarEstado(Pizza pizza)
        {
            // Ultimo estado en el diagrama
        }

        public string GetNombre()
        {
            return "En Camino";
        }
    }
}
