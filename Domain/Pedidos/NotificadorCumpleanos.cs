using System;
using Pizzeria.Domain.Productos.Estados;
using Pizzeria.Domain.Clientes;

namespace Pizzeria.Domain.Pedidos
{
    public class NotificadorCumpleanos : IObservador
    {
        public void Actualizar(EstadoPizza estado, Cliente cliente)
        {
            if (VerificarCumpleanos(cliente))
            {
                Console.WriteLine($"¡Feliz cumpleaños {cliente.Nombre}! Tienes un regalo especial con tu pedido que esta {estado.GetNombre()}.");
            }
        }

        public bool VerificarCumpleanos(Cliente cliente)
        {
            return cliente.EsCumpleanosHoy();
        }
    }
}
