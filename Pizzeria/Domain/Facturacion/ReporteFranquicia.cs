namespace Pizzeria.Domain.Facturacion;

using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Inventario;

public class ReporteFranquicia
{
    public Franquicia Franquicia { get; private set; }
    public string Periodo { get; private set; }
    public double TotalVentas { get; private set; }
    public int TotalPedidos { get; private set; }
    public List<StockInsumo> InsumosConStockBajo { get; private set; }
    public int EmpleadosActivos { get; private set; }

    public ReporteFranquicia(Franquicia franquicia, string periodo,
                             double totalVentas = 0, int totalPedidos = 0)
    {
        Franquicia = franquicia;
        Periodo = periodo;
        TotalVentas = totalVentas;
        TotalPedidos = totalPedidos;
        InsumosConStockBajo = franquicia.GetStockBajo();
        EmpleadosActivos = franquicia.GetEmpleados().Count;
    }

    public string GenerarResumen()
    {
        var stockBajoTexto = InsumosConStockBajo.Count > 0
            ? string.Join("\n", InsumosConStockBajo.Select(s => $"  - {s.Insumo.Nombre}: {s.CantidadActual} {s.Insumo.UnidadMedida}"))
            : "  (sin alertas)";

        return $@"
=== Reporte Franquicia: {Franquicia.Nombre} ===
Período:              {Periodo}
Ciudad:               {Franquicia.Ciudad}
Total ventas:         ${TotalVentas:N0}
Total pedidos:        {TotalPedidos}
Empleados activos:    {EmpleadosActivos}
Insumos stock bajo:   {InsumosConStockBajo.Count}
{stockBajoTexto}";
    }
}
