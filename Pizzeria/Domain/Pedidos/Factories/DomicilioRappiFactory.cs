namespace Pizzeria.Domain.Pedidos.Factories;

using Pizzeria.Domain.Clientes;
using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Pagos;

public class DomicilioRappiFactory : IPedidoFactory
{
    public string Tipo => "rappi";

    public IPedido Crear(int id, Cliente cliente, Franquicia franquicia, IMedioPago medioPago)
    {
        if (franquicia.ConvenioRappi == null || !franquicia.ConvenioRappi.EstaActivo())
            throw new InvalidOperationException("Esta franquicia no tiene convenio Rappi activo");
        return new DomicilioRappi(id, cliente, franquicia, medioPago, "Sin dirección", franquicia.ConvenioRappi);
    }
}
