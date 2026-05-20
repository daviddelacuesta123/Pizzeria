namespace Pizzeria.Domain.Pedidos.Factories;

using Pizzeria.Domain.Clientes;
using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Pagos;

public class LocalPedidoFactory : IPedidoFactory
{
    public string Tipo => "local";

    public IPedido Crear(int id, Cliente cliente, Franquicia franquicia, IMedioPago medioPago)
    {
        var mesas = franquicia.GetMesasDisponibles();
        if (mesas.Count == 0)
            throw new InvalidOperationException("No hay mesas disponibles");
        var meseros = franquicia.GetEmpleadosPorRol<Mesero>();
        if (meseros.Count == 0)
            throw new InvalidOperationException("No hay meseros disponibles");
        return new Local(id, cliente, franquicia, medioPago, mesas[0], meseros[0]);
    }
}
