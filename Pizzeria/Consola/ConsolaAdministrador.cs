namespace Pizzeria.Consola;

using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Inventario;
using Pizzeria.Servicios;

public class ConsolaAdministrador
{
    private readonly Gerente _gerente;
    private readonly FranquiciaService _franquiciaService;
    private readonly InventarioService _inventarioService;

    public ConsolaAdministrador(Gerente gerente, FranquiciaService franquiciaService,
                                InventarioService inventarioService)
    {
        _gerente = gerente;
        _franquiciaService = franquiciaService;
        _inventarioService = inventarioService;
    }

    public void MostrarMenuAdmin()
    {
        bool salir = false;
        while (!salir)
        {
            Console.WriteLine("\n╔═══════════════════════════════════╗");
            Console.WriteLine($"║      MENÚ ADMINISTRADOR           ║");
            Console.WriteLine($"║  Gerente: {_gerente.Nombre,-23}║");
            Console.WriteLine($"║  Franquicia: {_gerente.Franquicia.Nombre,-20}║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.WriteLine("1. Ver reporte de mi franquicia");
            Console.WriteLine("2. Ver reporte consolidado empresa");
            Console.WriteLine("3. Ver alertas de stock bajo");
            Console.WriteLine("4. Ver franquicias de la empresa");
            Console.WriteLine("0. Volver");
            Console.Write("> ");

            switch (Console.ReadLine())
            {
                case "1": VerReporteFranquicia(); break;
                case "2": VerReporteConsolidado(); break;
                case "3": VerAlertasStock(); break;
                case "4": VerFranquicias(); break;
                case "0": salir = true; break;
                default: Console.WriteLine("Opción inválida"); break;
            }
        }
    }

    private void VerReporteFranquicia()
    {
        var reporte = _gerente.VerReporteFranquicia(DateTime.Now.ToString("yyyy-MM"));
        Console.WriteLine(reporte.GenerarResumen());
    }

    private void VerReporteConsolidado()
    {
        var consolidado = _franquiciaService.GetReporteConsolidado(
            _gerente.Franquicia.Empresa,
            DateTime.Now.ToString("yyyy-MM"));
        Console.WriteLine(consolidado.GenerarResumen());
    }

    private void VerAlertasStock()
    {
        var alertas = _inventarioService.GenerarAlertasStock(_gerente.Franquicia);
        if (alertas.Count == 0)
        {
            Console.WriteLine("\n✓ No hay alertas de stock bajo");
            return;
        }
        Console.WriteLine($"\n⚠ {alertas.Count} insumo(s) con stock bajo:");
        foreach (var s in alertas)
            Console.WriteLine($"  - {s.Insumo.Nombre}: {s.CantidadActual} {s.Insumo.UnidadMedida} (mínimo: {s.CantidadMinima})");
    }

    private void VerFranquicias()
    {
        Console.WriteLine("\n=== Franquicias de la empresa ===");
        foreach (var f in _gerente.Franquicia.Empresa.GetFranquicias())
            Console.WriteLine($"  - {f.Nombre} | {f.Ciudad} | Empleados: {f.GetEmpleados().Count} | Mesas: {f.GetMesas().Count}");
    }
}
