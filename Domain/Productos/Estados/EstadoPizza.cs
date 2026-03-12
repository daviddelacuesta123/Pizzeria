namespace Pizzeria.Domain.Productos.Estados
{
    public interface EstadoPizza
    {
        void ManejarEstado(Pizza pizza);
        string GetNombre();
    }
}
