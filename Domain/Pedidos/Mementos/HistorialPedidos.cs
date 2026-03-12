using System.Collections.Generic;

namespace Pizzeria.Domain.Pedidos.Mementos
{
    public class HistorialPedidos
    {
        private List<PedidoMemento> _mementos = new List<PedidoMemento>();

        public void Guardar(PedidoMemento memento)
        {
            _mementos.Add(memento);
        }

        public PedidoMemento Restaurar(int indice)
        {
            if (indice >= 0 && indice < _mementos.Count)
            {
                return _mementos[indice];
            }
            return null;
        }

        public List<PedidoMemento> Listar()
        {
            return _mementos;
        }
    }
}
