namespace Pizzeria.Domain.Pedidos.Mementos;

public class HistorialPedidos
{
    private readonly List<PedidoMemento> _mementos = new();

    public void Guardar(PedidoMemento memento)
    {
        if (memento == null) throw new ArgumentNullException(nameof(memento));
        _mementos.Add(memento);
    }

    public PedidoMemento? Restaurar(int indice)
    {
        if (indice < 0 || indice >= _mementos.Count) return null;
        return _mementos[indice];
    }

    public IReadOnlyList<PedidoMemento> Listar() => _mementos.AsReadOnly();
}
