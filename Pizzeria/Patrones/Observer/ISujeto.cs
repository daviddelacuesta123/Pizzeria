namespace Pizzeria.Patrones.Observer;

public interface ISujeto<T>
{
    void Suscribir(IObservador<T> obs);
    void Desuscribir(IObservador<T> obs);
    void Notificar(T contexto);
}
