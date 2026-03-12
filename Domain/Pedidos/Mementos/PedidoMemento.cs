using System;
using System.Collections.Generic;

namespace Pizzeria.Domain.Pedidos.Mementos
{
    public class PedidoMemento
    {
        private string _estadoPedido;
        private List<ItemPedido> _items;
        private DateTime _fecha;
        private double _totalCliente;

        public PedidoMemento(string estado, List<ItemPedido> items, double totalCliente)
        {
            _estadoPedido = estado;
            // Shallow copy to capture snapshot of items list
            _items = new List<ItemPedido>(items);
            _fecha = DateTime.Now;
            _totalCliente = totalCliente;
        }

        public string GetEstado()
        {
            return _estadoPedido;
        }

        public List<ItemPedido> GetItems()
        {
            return _items;
        }

        public DateTime GetFecha()
        {
            return _fecha;
        }

        public double GetTotalCliente()
        {
            return _totalCliente;
        }
    }
}
