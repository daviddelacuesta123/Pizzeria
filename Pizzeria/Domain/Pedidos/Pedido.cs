namespace Pizzeria.Domain.Pedidos;

using Pizzeria.Domain.Clientes;
using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Productos;
using Pizzeria.Domain.Pagos;
using Pizzeria.Domain.Pedidos.Mementos;
using Pizzeria.Patrones.Observer;
using Pizzeria.Eventos;

public abstract class Pedido : IPedido, ISujeto<PizzaListaEvento>, Pizzeria.Domain.Shared.IAggregateRoot
{
    public int Id { get; private set; }
    public Cliente Cliente { get; private set; }
    public Franquicia Franquicia { get; private set; }
    public DateTime Fecha { get; private set; }
    public IMedioPago MedioPago { get; private set; }
    public string Estado { get; protected set; } = "Creado";

    protected readonly List<ItemPedido> _items = new();
    private readonly List<IObservador<PizzaListaEvento>> _observadores = new();

    protected Pedido(int id, Cliente cliente, Franquicia franquicia, IMedioPago medioPago)
    {
        Id = id;
        Cliente = cliente;
        Franquicia = franquicia;
        MedioPago = medioPago;
        Fecha = DateTime.Now;
    }

    public IReadOnlyList<ItemPedido> GetItems() => _items.AsReadOnly();

    public virtual void AgregarItem(Producto p, int cantidad)
    {
        if (p == null) throw new ArgumentNullException(nameof(p));
        _items.Add(new ItemPedido(p, cantidad));
    }

    public virtual double CalcularTotalCliente() => _items.Sum(i => i.Subtotal);
    public virtual double CalcularCostoParaPizzeria() => CalcularTotalCliente();

    public void Suscribir(IObservador<PizzaListaEvento> obs)
    {
        if (obs == null) throw new ArgumentNullException(nameof(obs));
        _observadores.Add(obs);
    }

    public void Desuscribir(IObservador<PizzaListaEvento> obs) => _observadores.Remove(obs);

    public void Notificar(PizzaListaEvento contexto)
    {
        foreach (var obs in _observadores)
            obs.Actualizar(contexto);
    }

    public PedidoMemento GuardarMemento() =>
        new PedidoMemento(Estado, new List<ItemPedido>(_items), Fecha, CalcularTotalCliente());
}
