namespace Pizzeria.Patrones.Observer;

public interface IObservador<T>
{
    void Actualizar(T contexto);
}
