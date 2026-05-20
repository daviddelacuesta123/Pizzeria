namespace Pizzeria.Domain.Facturacion;

using Pizzeria.Domain.Organizacion;

public class ReporteConsolidado
{
    public Empresa Empresa { get; private set; }
    public string Periodo { get; private set; }
    public List<ReporteFranquicia> ReportesPorFranquicia { get; private set; }
    public double TotalVentasGlobal { get; private set; }

    public ReporteConsolidado(Empresa empresa, string periodo, List<ReporteFranquicia> reportes)
    {
        Empresa = empresa;
        Periodo = periodo;
        ReportesPorFranquicia = reportes;
        TotalVentasGlobal = reportes.Sum(r => r.TotalVentas);
    }

    public string GenerarResumen()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("╔════════════════════════════════════════╗");
        sb.AppendLine($"  REPORTE CONSOLIDADO: {Empresa.Nombre}");
        sb.AppendLine($"  Período: {Periodo}");
        sb.AppendLine($"  Total Ventas Global: ${TotalVentasGlobal:N0}");
        sb.AppendLine("╚════════════════════════════════════════╝");
        foreach (var r in ReportesPorFranquicia)
            sb.AppendLine(r.GenerarResumen());
        var rentable = GetFranquiciaMasRentable();
        if (rentable != null)
            sb.AppendLine($"\n🏆 Franquicia más rentable: {rentable.Nombre}");
        return sb.ToString();
    }

    public Franquicia? GetFranquiciaMasRentable() =>
        ReportesPorFranquicia.OrderByDescending(r => r.TotalVentas).FirstOrDefault()?.Franquicia;
}
