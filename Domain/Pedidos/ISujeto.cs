namespace Pizzeria.Domain.Pedidos
{
    public interface ISujeto
    {
        void Suscribir(IObservador obs);
        void Desuscribir(IObservador obs);
        void Notificar();
    }
}
