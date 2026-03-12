using System;
using System.Collections.Generic;
using System.Linq;
using Pizzeria.Domain.Clientes;

namespace Pizzeria.Domain.Pedidos
{
    public abstract class Pedido : ISujeto
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public List<ItemPedido> Items { get; set; } = new();
        public DateTime Fecha { get; set; } = DateTime.Now;

        private List<IObservador> observadores = new List<IObservador>();

        public void AgregarProducto(ItemPedido item)
        {
            Items.Add(item);
        }

        public double CalcularSubtotal()
        {
            return Items.Sum(i => i.Subtotal());
        }

        public abstract double CalcularCostoAdicional();
        public abstract double CalcularCostoFinal();

        public void Suscribir(IObservador obs)
        {
            observadores.Add(obs);
        }

        public void Desuscribir(IObservador obs)
        {
            observadores.Remove(obs);
        }

        public void Notificar()
        {
            Productos.Estados.EstadoPizza estado = null;
            var itemPizza = Items.FirstOrDefault(i => i.Producto is Productos.Pizza);
            if (itemPizza != null)
            {
                estado = ((Productos.Pizza)itemPizza.Producto).GetEstado();
            }

            foreach (var obs in observadores)
            {
                obs.Actualizar(estado, Cliente);
            }
        }

        public Mementos.PedidoMemento GuardarMemento()
        {
            string estado = "Creado";
            var itemPizza = Items.FirstOrDefault(i => i.Producto is Productos.Pizza);
            if (itemPizza != null)
            {
                estado = ((Productos.Pizza)itemPizza.Producto).GetEstado().GetNombre();
            }

            return new Mementos.PedidoMemento(estado, Items, CalcularCostoFinal());
        }
    }
}
