namespace Pizzeria.Domain.Organizacion;

using Pizzeria.Domain.Inventario;
using Pizzeria.Domain.Facturacion;

public class Gerente : Empleado
{
    public Gerente(int id, string nombre, string cedula, string telefono, double salario, Franquicia franquicia)
        : base(id, nombre, cedula, telefono, salario, franquicia) { }

    public override string GetRol() => "Gerente";

    public bool AprobarOrdenCompra(OrdenCompra orden)
    {
        if (orden == null) return false;
        if (orden.Estado != EstadoOrden.PENDIENTE) return false;
        orden.Aprobar();
        return true;
    }

    public ReporteFranquicia VerReporteFranquicia(string periodo) => new ReporteFranquicia(Franquicia, periodo);
}
