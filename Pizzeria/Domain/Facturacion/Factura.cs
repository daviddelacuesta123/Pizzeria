namespace Pizzeria.Domain.Facturacion;

using Pizzeria.Domain.Pedidos;
using Pizzeria.Domain.Clientes;

public class Factura : Pizzeria.Domain.Shared.IAggregateRoot
{
    public int Id { get; private set; }
    public string NumeroFactura { get; private set; }
    public string Prefijo { get; private set; }
    public string Cufe { get; private set; }
    public DateTime Fecha { get; private set; }
    public string NitEmpresa { get; private set; }
    public string NitCliente { get; private set; }
    public string NombreCliente { get; private set; }
    public double TotalCliente { get; private set; }
    public double TotalNetoPizzeria { get; private set; }
    public double Iva { get; private set; }
    public IPedido Pedido { get; private set; }

    public Factura(int id, IPedido pedido, ResolucionDIAN resolucion, string nitEmpresa,
                   Cliente cliente, string nitCliente, double porcentajeIva = 0.19)
    {
        Id = id;
        Pedido = pedido;
        Prefijo = resolucion.Prefijo;
        NumeroFactura = resolucion.GetSiguienteConsecutivo().ToString();
        Fecha = DateTime.Now;
        NitEmpresa = nitEmpresa;
        NitCliente = nitCliente;
        NombreCliente = cliente.Nombre;
        TotalCliente = pedido.CalcularTotalCliente();
        TotalNetoPizzeria = pedido.CalcularCostoParaPizzeria();
        Iva = TotalCliente * porcentajeIva;
        Cufe = GenerarCUFE();
    }

    public string GenerarCUFE()
    {
        var datos = $"{NitEmpresa}-{Prefijo}{NumeroFactura}-{TotalCliente}-{Fecha:yyyyMMddHHmmss}";
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(datos))[..20];
    }

    public bool EsValidaDIAN() =>
        !string.IsNullOrEmpty(Cufe) && !string.IsNullOrEmpty(NitEmpresa) && TotalCliente > 0;

    public override string ToString() => $@"
═══════════════════════════════════
        FACTURA ELECTRÓNICA DIAN
═══════════════════════════════════
Número: {Prefijo}{NumeroFactura}
CUFE: {Cufe}
Fecha: {Fecha:yyyy-MM-dd HH:mm:ss}
NIT Empresa: {NitEmpresa}
NIT Cliente: {NitCliente}
Cliente: {NombreCliente}
-----------------------------------
Total Cliente:   ${TotalCliente:N0}
IVA (19%):       ${Iva:N0}
Neto Pizzería:   ${TotalNetoPizzeria:N0}
═══════════════════════════════════";
}
