using System;
using Pizzeria.Domain.Productos.Estados;
using Pizzeria.Domain.Clientes;

namespace Pizzeria.Domain.Pedidos
{
    public class NotificadorCliente : IObservador
    {
        private string _canal;

        public NotificadorCliente(string canal)
        {
            _canal = canal;
        }

        public void Actualizar(EstadoPizza estado, Cliente cliente)
        {
            Console.WriteLine($"Notificando al cliente {cliente.Nombre} por canal {_canal}: Su pizza esta {estado.GetNombre()}");
        }
    }
}
