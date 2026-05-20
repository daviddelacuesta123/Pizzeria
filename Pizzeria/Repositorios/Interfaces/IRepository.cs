namespace Pizzeria.Repositorios.Interfaces;

public interface IRepository<T> where T : class
{
    void Agregar(T entidad);
    T? ObtenerPorId(int id);
    IReadOnlyList<T> ObtenerTodos();
}
