using Pizzeria.Domain.Productos.Estados;
using Pizzeria.Domain.Clientes;

namespace Pizzeria.Domain.Pedidos
{
    public interface IObservador
    {
        void Actualizar(EstadoPizza estado, Cliente cliente);
    }
}
