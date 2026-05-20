namespace Pizzeria.Patrones.Observer;

using Pizzeria.Eventos;

public class NotificadorCliente : IObservador<PizzaListaEvento>
{
    public string Canal { get; private set; }

    public NotificadorCliente(string canal = "SMS")
    {
        Canal = canal;
    }

    public void Actualizar(PizzaListaEvento evento)
    {
        var cliente = evento.Pedido.Cliente;
        Console.WriteLine($"[Notificación {Canal}] Cliente {cliente.Nombre}: tu pizza está lista (pedido #{evento.Pedido.Id})");
    }
}
