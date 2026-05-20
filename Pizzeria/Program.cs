using Pizzeria.Repositorios.EnMemoria;
using Pizzeria.Domain.Pedidos.Factories;
using Pizzeria.Domain.ValueObjects;
using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Inventario;
using Pizzeria.Domain.Facturacion;
using Pizzeria.Domain.Clientes;
using Pizzeria.Servicios;
using Pizzeria.Consola;
using Pizzeria.Patrones.Command;
using Pizzeria.Patrones.Observer;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("╔═══════════════════════════════════════╗");
Console.WriteLine("║    SISTEMA PIZZERIA - VERSIÓN 2.0    ║");
Console.WriteLine("╚═══════════════════════════════════════╝\n");

// ── 1. Empresa ────────────────────────────────────────────
var empresa = new Empresa(
    1,
    "Pizzeria SAS",
    new Nit("900123456"),
    new Direccion("Calle 1 #1-1", "Cartagena"),
    new Telefono("3001234567"));
var resolucion = new ResolucionDIAN("18760000001", "FE", 1, 5000000, DateTime.Now.AddYears(2));
empresa.AgregarResolucion(resolucion);

// ── 2. Repositorios y Servicios ───────────────────────────
var pedidoRepo     = new PedidoRepositoryEnMemoria();
var franquiciaRepo = new FranquiciaRepositoryEnMemoria();
var ordenRepo      = new OrdenCompraRepositoryEnMemoria();
var facturaRepo    = new FacturaRepositoryEnMemoria();

var franquiciaService = new FranquiciaService(franquiciaRepo);
var factoriesPedido = new IPedidoFactory[]
{
    new LocalPedidoFactory(),
    new DomicilioPropioFactory(),
    new DomicilioRappiFactory()
};
var pedidoService = new PedidoService(pedidoRepo, factoriesPedido);
var facturaService    = new FacturaService(facturaRepo);
var fidelizacion      = new FidelizacionService();
var inventario        = new InventarioService(ordenRepo);

// ── 3. Franquicia Centro ──────────────────────────────────
var franquiciaCentro = franquiciaService.CrearFranquicia(
    empresa, "Pizzeria Centro", "Cra 5 #10-20", "Cartagena", "3009876543");

// ── 4. Empleados ──────────────────────────────────────────
var gerente  = new Gerente(1, "Carlos Pérez", "10101010", "3001111111", 4000000, franquiciaCentro);
var chef     = new Chef(2, "María López", "20202020", "3002222222", 2500000, franquiciaCentro, "Pizzas artesanales");
var mesero   = new Mesero(3, "Juan García", "30303030", "3003333333", 1800000, franquiciaCentro, "Mañana");
var repartidor = new Repartidor(4, "Luis Torres", "40404040", "3004444444", 1600000, franquiciaCentro, "Moto");

franquiciaService.AsignarEmpleado(franquiciaCentro, gerente);
franquiciaService.AsignarEmpleado(franquiciaCentro, chef);
franquiciaService.AsignarEmpleado(franquiciaCentro, mesero);
franquiciaService.AsignarEmpleado(franquiciaCentro, repartidor);

// ── 5. Mesas ──────────────────────────────────────────────
franquiciaCentro.AgregarMesa(new Mesa(1, 1, 4, franquiciaCentro));
franquiciaCentro.AgregarMesa(new Mesa(2, 2, 6, franquiciaCentro));
franquiciaCentro.AgregarMesa(new Mesa(3, 3, 2, franquiciaCentro));

// ── 6. Inventario ─────────────────────────────────────────
var insumoHarina   = new Insumo(1, "Harina", "kg", "Harina de trigo");
var insumoQueso    = new Insumo(2, "Queso", "kg", "Queso mozzarella");
var insumoTomate   = new Insumo(3, "Salsa de tomate", "litros", "Salsa italiana");

franquiciaCentro.AgregarStockInsumo(new StockInsumo(insumoHarina, 50, 10, franquiciaCentro));
franquiciaCentro.AgregarStockInsumo(new StockInsumo(insumoQueso,  20,  5, franquiciaCentro));
franquiciaCentro.AgregarStockInsumo(new StockInsumo(insumoTomate, 30,  8, franquiciaCentro));

// ── 7. Convenio Rappi ─────────────────────────────────────
var convenioRappi = new ConvenioRappi(1, franquiciaCentro, 0.25, DateTime.Now);
franquiciaCentro.EstablecerConvenioRappi(convenioRappi);

// ── 8. Franquicia Norte ───────────────────────────────────
var franquiciaNorte = franquiciaService.CrearFranquicia(
    empresa, "Pizzeria Norte", "Calle 80 #45-10", "Cartagena", "3008765432");
var gerenteNorte = new Gerente(5, "Ana Martínez", "50505050", "3005555555", 4000000, franquiciaNorte);
franquiciaService.AsignarEmpleado(franquiciaNorte, gerenteNorte);

// ── 9. Observer: notificador de stock bajo ────────────────
inventario.SuscribirObservador(new NotificadorStockBajo());

// ── 10. Cocina ────────────────────────────────────────────
var cocina = new Cocina(franquiciaCentro);

// ── 11. Consolas ──────────────────────────────────────────
var consolaAdmin      = new ConsolaAdministrador(gerente, franquiciaService, inventario);
var consolaFranquicia = new ConsolaFranquicia(empresa, franquiciaCentro, pedidoService,
                                               facturaService, fidelizacion, inventario, cocina);

// ── 12. Menú principal ────────────────────────────────────
bool salir = false;
while (!salir)
{
    Console.WriteLine("\n╔═══════════════════════════════════════╗");
    Console.WriteLine("║           MENÚ PRINCIPAL             ║");
    Console.WriteLine("╚═══════════════════════════════════════╝");
    Console.WriteLine("1. Consola Administrador");
    Console.WriteLine("2. Consola Franquicia");
    Console.WriteLine("0. Salir");
    Console.Write("> ");

    switch (Console.ReadLine())
    {
        case "1": consolaAdmin.MostrarMenuAdmin(); break;
        case "2": consolaFranquicia.MostrarMenuFranquicia(); break;
        case "0": salir = true; break;
        default: Console.WriteLine("Opción inválida"); break;
    }
}

Console.WriteLine("\nHasta luego!");
