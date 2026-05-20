namespace Pizzeria.Patrones.States;

using Pizzeria.Domain.Productos;

public interface IEstadoPizza
{
    void ManejarEstado(Pizza pizza);
    string GetNombre();
}
