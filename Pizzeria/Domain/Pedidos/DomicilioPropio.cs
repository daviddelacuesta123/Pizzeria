namespace Pizzeria.Domain.Pedidos;

using Pizzeria.Domain.Clientes;
using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Pagos;

public class DomicilioPropio : Pedido
{
    public string Direccion { get; private set; }
    public double CostoEnvio { get; private set; }
    public Repartidor? Repartidor { get; private set; }

    public DomicilioPropio(int id, Cliente cliente, Franquicia franquicia, IMedioPago medioPago,
                           string direccion, double costoEnvio)
        : base(id, cliente, franquicia, medioPago)
    {
        Direccion = direccion;
        CostoEnvio = costoEnvio;
    }

    public override double CalcularTotalCliente() => base.CalcularTotalCliente() + CostoEnvio;

    public void AsignarRepartidor(Repartidor r)
    {
        if (r == null) throw new ArgumentNullException(nameof(r));
        if (!r.EstaDisponible()) throw new InvalidOperationException("El repartidor no está disponible");
        Repartidor = r;
        r.MarcarOcupado();
    }
}
