namespace Pizzeria.Domain.Pedidos;

using Pizzeria.Domain.Clientes;
using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Inventario;
using Pizzeria.Domain.Pagos;

public class DomicilioRappi : Pedido
{
    public string Direccion { get; private set; }
    public ConvenioRappi Convenio { get; private set; }

    public DomicilioRappi(int id, Cliente cliente, Franquicia franquicia, IMedioPago medioPago,
                          string direccion, ConvenioRappi convenio)
        : base(id, cliente, franquicia, medioPago)
    {
        Direccion = direccion;
        Convenio = convenio;
    }

    public override double CalcularCostoParaPizzeria() => CalcularTotalCliente() - CalcularComisionPlataforma();
    public double CalcularComisionPlataforma() => Convenio.CalcularComision(CalcularTotalCliente());
}
