namespace Pizzeria.Domain.Pedidos.Factories;

using Pizzeria.Domain.Clientes;
using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Pagos;

public class DomicilioPropioFactory : IPedidoFactory
{
    public string Tipo => "domicilio";

    public IPedido Crear(int id, Cliente cliente, Franquicia franquicia, IMedioPago medioPago)
    {
        return new DomicilioPropio(id, cliente, franquicia, medioPago, "Sin dirección", 5000);
    }
}
