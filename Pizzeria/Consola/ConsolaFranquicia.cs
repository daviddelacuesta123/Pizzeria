namespace Pizzeria.Consola;

using Pizzeria.Domain.Organizacion;
using Pizzeria.Domain.Inventario;
using Pizzeria.Domain.Clientes;
using Pizzeria.Domain.Productos;
using Pizzeria.Domain.Productos.Decorators;
using Pizzeria.Domain.Pagos;
using Pizzeria.Domain.Productos.Builders;
using Pizzeria.Domain.ValueObjects;
using Pizzeria.Domain.Pedidos;
using Pizzeria.Domain.Facturacion;
using Pizzeria.Servicios;
using Pizzeria.Patrones.Command;
using Pizzeria.Patrones.States;

public class ConsolaFranquicia
{
    private readonly Empresa _empresa;
    private readonly Franquicia _franquicia;
    private readonly PedidoService _pedidoService;
    private readonly FacturaService _facturaService;
    private readonly FidelizacionService _fidelizacionService;
    private readonly InventarioService _inventarioService;
    private readonly Cocina _cocina;

    private readonly Dictionary<int, MesaAbierta> _mesasAbiertas = new();
    private readonly List<Cliente> _clientes = new();

    private record MesaAbierta(Local Pedido, Cliente Cliente, Mesa Mesa);

    public ConsolaFranquicia(Empresa empresa, Franquicia franquicia, PedidoService pedidoService,
                              FacturaService facturaService, FidelizacionService fidelizacionService,
                              InventarioService inventarioService, Cocina cocina)
    {
        _empresa = empresa;
        _franquicia = franquicia;
        _pedidoService = pedidoService;
        _facturaService = facturaService;
        _fidelizacionService = fidelizacionService;
        _inventarioService = inventarioService;
        _cocina = cocina;
    }

    public void MostrarMenuFranquicia()
    {
        bool salir = false;
        while (!salir)
        {
            Console.WriteLine("\n╔═══════════════════════════════════╗");
            Console.WriteLine($"║  FRANQUICIA: {_franquicia.Nombre,-21}║");
            Console.WriteLine($"║  {_franquicia.Ciudad,-33}║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.WriteLine("1. Nuevo pedido");
            Console.WriteLine("2. Agregar a mesa abierta");
            Console.WriteLine("3. Cerrar mesa y pagar");
            Console.WriteLine("4. Ver mesas");
            Console.WriteLine("5. Ver pedidos");
            Console.WriteLine("6. Ver cocina");
            Console.WriteLine("7. Ver empleados");
            Console.WriteLine("0. Volver");
            Console.Write("> ");

            switch (Console.ReadLine())
            {
                case "1": NuevoPedido(); break;
                case "2": AgregarAMesaAbierta(); break;
                case "3": CerrarMesa(); break;
                case "4": VerMesas(); break;
                case "5": VerPedidos(); break;
                case "6": VerCocina(); break;
                case "7": VerEmpleados(); break;
                case "0": salir = true; break;
                default: Console.WriteLine("Opción inválida"); break;
            }
        }
    }

    // ── 1. Nuevo pedido ───────────────────────────────────────

    private void NuevoPedido()
    {
        Console.WriteLine("\n=== NUEVO PEDIDO ===");

        var cliente = SeleccionarORegistrarCliente();
        if (cliente == null) return;

        Console.WriteLine("\nTipo de pedido:");
        Console.WriteLine("  1) Local (mesa)");
        Console.WriteLine("  2) Domicilio propio");
        Console.WriteLine("  3) Rappi");
        Console.Write("> ");
        var tipo = Console.ReadLine() switch
        {
            "1" => "local",
            "2" => "domicilio",
            "3" => "rappi",
            _   => "local"
        };

        try
        {
            if (tipo == "local")
            {
                var mesa = SeleccionarMesa();
                if (mesa == null) return;

                var pago = SeleccionarMedioPago();
                var pedido = (Local)_pedidoService.CrearPedido(cliente, _franquicia, tipo, pago);
                pedido.AsignarMesa(mesa);
                mesa.Ocupar();
                _mesasAbiertas[mesa.Numero] = new MesaAbierta(pedido, cliente, mesa);

                Console.WriteLine($"\n✓ Mesa {mesa.Numero} abierta para {cliente.Nombre}.");
                Console.WriteLine("  Usa 'Agregar a mesa abierta' para añadir productos.");
            }
            else
            {
                var pago = SeleccionarMedioPago();
                var pedido = _pedidoService.CrearPedido(cliente, _franquicia, tipo, pago);

                LoopAgregarProductos(pedido);

                if (!pedido.GetItems().Any())
                {
                    Console.WriteLine("\n✗ Pedido sin productos. Cancelado.");
                    return;
                }

                ProcesarPagoYConfirmar(pedido, cliente, pago);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Error: {ex.Message}");
        }
    }

    // ── 2. Agregar a mesa abierta ─────────────────────────────

    private void AgregarAMesaAbierta()
    {
        if (_mesasAbiertas.Count == 0)
        {
            Console.WriteLine("\n  No hay mesas abiertas.");
            return;
        }

        Console.WriteLine("\n=== MESAS ABIERTAS ===");
        foreach (var (num, m) in _mesasAbiertas)
            Console.WriteLine($"  Mesa {num} — {m.Cliente.Nombre} — {m.Pedido.GetItems().Count} producto(s) — ${m.Pedido.CalcularTotalCliente():N0}");

        Console.Write("\n¿Número de mesa? ");
        if (!int.TryParse(Console.ReadLine(), out int numero) || !_mesasAbiertas.ContainsKey(numero))
        {
            Console.WriteLine("  Mesa no encontrada.");
            return;
        }

        LoopAgregarProductos(_mesasAbiertas[numero].Pedido);
        Console.WriteLine($"\n  Subtotal mesa {numero}: ${_mesasAbiertas[numero].Pedido.CalcularTotalCliente():N0}");
    }

    // ── 3. Cerrar mesa y pagar ────────────────────────────────

    private void CerrarMesa()
    {
        if (_mesasAbiertas.Count == 0)
        {
            Console.WriteLine("\n  No hay mesas abiertas.");
            return;
        }

        Console.WriteLine("\n=== CERRAR MESA ===");
        foreach (var (num, m) in _mesasAbiertas)
            Console.WriteLine($"  Mesa {num} — {m.Cliente.Nombre} — ${m.Pedido.CalcularTotalCliente():N0}");

        Console.Write("\n¿Número de mesa a cerrar? ");
        if (!int.TryParse(Console.ReadLine(), out int numero) || !_mesasAbiertas.ContainsKey(numero))
        {
            Console.WriteLine("  Mesa no encontrada.");
            return;
        }

        var (pedido, cliente, mesa) = _mesasAbiertas[numero];

        if (!pedido.GetItems().Any())
        {
            Console.WriteLine("\n  Mesa sin productos. Liberando sin cargo.");
            mesa.Liberar();
            _mesasAbiertas.Remove(numero);
            return;
        }

        // Resumen de productos
        Console.WriteLine("\n─── Productos ────────────────────────────");
        foreach (var item in pedido.GetItems())
            Console.WriteLine($"  {item.Cantidad}x {item.Producto.Nombre,-30} ${item.Subtotal:N0}");

        double subtotal = pedido.CalcularTotalCliente();
        Console.WriteLine($"  {"Subtotal",-34} ${subtotal:N0}");

        // Descuento cumpleaños (solo si es hoy su cumpleaños)
        double totalConDescuento = _fidelizacionService.AplicarDescuentoCumpleanos(cliente, subtotal);
        double descuentoCumple = subtotal - totalConDescuento;
        if (descuentoCumple > 0)
            Console.WriteLine($"  {"Descuento cumpleaños (10%)",-34} -${descuentoCumple:N0}");

        // Canje de puntos
        double totalFinal = totalConDescuento;
        double descuentoPuntos = 0;
        if (cliente.PuntosAcumulados > 0)
        {
            Console.Write($"\n¿Canjear puntos? Tienes {cliente.PuntosAcumulados} pts = ${cliente.PuntosAcumulados * 100:N0} de descuento [s/n]: ");
            if (Console.ReadLine()?.Trim().ToLower() == "s")
            {
                Console.Write("¿Cuántos puntos? ");
                if (int.TryParse(Console.ReadLine(), out int pts) && pts > 0)
                {
                    double antesDeCanjes = totalFinal;
                    totalFinal = _fidelizacionService.CanjearPuntos(cliente, pts, totalFinal);
                    descuentoPuntos = antesDeCanjes - totalFinal;
                }
            }
        }

        Console.WriteLine($"\n  {"TOTAL A PAGAR",-34} ${totalFinal:N0}");

        // Medio de pago
        var pago = SeleccionarMedioPago();
        pago.ProcesarPago(totalFinal);
        _fidelizacionService.AcumularPuntos(cliente, totalFinal);
        _pedidoService.ConfirmarPedido(pedido.Id);

        // Factura
        var resolucion = _empresa.GetResolucionVigente();
        if (resolucion != null)
        {
            try
            {
                var factura = _facturaService.GenerarFactura(
                    pedido, resolucion, _empresa.Nit.Valor, cliente, "000000000");
                ImprimirFactura(factura, descuentoCumple, descuentoPuntos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [Factura] No se pudo generar: {ex.Message}");
            }
        }

        mesa.Liberar();
        _mesasAbiertas.Remove(numero);
        Console.WriteLine($"\n✓ Mesa {numero} liberada.");
    }

    private void ImprimirFactura(Factura factura, double descuentoCumple, double descuentoPuntos)
    {
        Console.WriteLine("\n╔══════════════════════════════════════════╗");
        Console.WriteLine("║       FACTURA ELECTRÓNICA DIAN           ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.WriteLine($"  Número  : {factura.Prefijo}{factura.NumeroFactura}");
        Console.WriteLine($"  CUFE    : {factura.Cufe}");
        Console.WriteLine($"  Fecha   : {factura.Fecha:yyyy-MM-dd HH:mm}");
        Console.WriteLine($"  NIT     : {factura.NitEmpresa}");
        Console.WriteLine($"  Cliente : {factura.NombreCliente}");
        Console.WriteLine("──────────────────────────────────────────");
        foreach (var item in factura.Pedido.GetItems())
            Console.WriteLine($"  {item.Cantidad}x {item.Producto.Nombre,-32} ${item.Subtotal:N0}");
        Console.WriteLine("──────────────────────────────────────────");
        Console.WriteLine($"  {"Subtotal",-36} ${factura.TotalCliente:N0}");
        if (descuentoCumple > 0)
            Console.WriteLine($"  {"Desc. cumpleaños (10%)",-36} -${descuentoCumple:N0}");
        if (descuentoPuntos > 0)
            Console.WriteLine($"  {"Desc. puntos canjeados",-36} -${descuentoPuntos:N0}");
        Console.WriteLine($"  {"IVA (19%)",-36} ${factura.Iva:N0}");
        Console.WriteLine($"  {"TOTAL NETO PIZZERÍA",-36} ${factura.TotalNetoPizzeria:N0}");
        Console.WriteLine("══════════════════════════════════════════");
    }

    // ── Helpers ───────────────────────────────────────────────

    private Cliente? SeleccionarORegistrarCliente()
    {
        Console.WriteLine("\nCliente:");
        Console.WriteLine("  1) Nuevo cliente");
        if (_clientes.Count > 0)
            Console.WriteLine("  2) Buscar cliente registrado");
        Console.Write("> ");

        if (Console.ReadLine() == "2" && _clientes.Count > 0)
        {
            Console.Write("Nombre o teléfono: ");
            var busqueda = Console.ReadLine()?.ToLower() ?? "";
            var encontrado = _clientes.FirstOrDefault(c =>
                c.Nombre.ToLower().Contains(busqueda) ||
                c.Telefono.Valor.Contains(busqueda));

            if (encontrado != null)
            {
                Console.WriteLine($"  ✓ {encontrado.Nombre} — {encontrado.PuntosAcumulados} puntos");
                return encontrado;
            }
            Console.WriteLine("  No encontrado. Registrando nuevo cliente.");
        }

        return RegistrarCliente();
    }

    private Cliente? RegistrarCliente()
    {
        Console.Write("Nombre: ");
        var nombre = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(nombre)) { Console.WriteLine("✗ Nombre obligatorio."); return null; }

        Console.Write("Teléfono: ");
        Telefono telefonoVO;
        try { telefonoVO = new Telefono(Console.ReadLine() ?? ""); }
        catch (ArgumentException ex) { Console.WriteLine($"✗ Teléfono inválido: {ex.Message}"); return null; }

        Console.Write("Email (Enter para omitir): ");
        var email = Console.ReadLine() ?? "";

        Console.Write("Fecha de nacimiento dd/MM/yyyy (Enter para omitir): ");
        var fechaStr = Console.ReadLine() ?? "";
        DateTime fechaNac = new DateTime(1990, 1, 1);
        if (!string.IsNullOrWhiteSpace(fechaStr) &&
            !DateTime.TryParseExact(fechaStr, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out fechaNac))
        {
            Console.WriteLine("  Fecha inválida, se omitirá descuento cumpleaños.");
            fechaNac = new DateTime(1990, 1, 1);
        }

        var cliente = new Cliente(new Random().Next(1000, 9999), nombre, telefonoVO, email, fechaNac);
        _clientes.Add(cliente);
        Console.WriteLine($"  ✓ Cliente {cliente.Nombre} registrado.");
        return cliente;
    }

    private Mesa? SeleccionarMesa()
    {
        var disponibles = _franquicia.GetMesasDisponibles();
        if (disponibles.Count == 0) { Console.WriteLine("\n✗ No hay mesas disponibles."); return null; }

        Console.WriteLine("\nMesas disponibles:");
        foreach (var m in disponibles)
            Console.WriteLine($"  Mesa {m.Numero} — capacidad {m.Capacidad}");
        Console.Write("¿Número de mesa? ");

        if (!int.TryParse(Console.ReadLine(), out int num))
        { Console.WriteLine("Número inválido."); return null; }

        var mesa = disponibles.FirstOrDefault(m => m.Numero == num);
        if (mesa == null) { Console.WriteLine("Mesa no disponible."); return null; }
        return mesa;
    }

    private IMedioPago SeleccionarMedioPago()
    {
        Console.WriteLine("\nMedio de pago:");
        Console.WriteLine("  1) Efectivo");
        Console.WriteLine("  2) Tarjeta");
        Console.WriteLine("  3) Transferencia");
        Console.Write("> ");
        return Console.ReadLine() switch
        {
            "2" => new Tarjeta(),
            "3" => new Transferencia(),
            _   => new Efectivo()
        };
    }

    private void ProcesarPagoYConfirmar(IPedido pedido, Cliente cliente, IMedioPago pago)
    {
        double total = pedido.CalcularTotalCliente();
        double totalFinal = _fidelizacionService.AplicarDescuentoCumpleanos(cliente, total);
        pago.ProcesarPago(totalFinal);
        _fidelizacionService.AcumularPuntos(cliente, totalFinal);
        _pedidoService.ConfirmarPedido(pedido.Id);
        Console.WriteLine($"\n✓ Pedido #{pedido.Id} confirmado — ${totalFinal:N0} — {pago.GetNombre()}");
    }

    private void LoopAgregarProductos(IPedido pedido)
    {
        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("\n¿Qué deseas agregar?");
            Console.WriteLine("  1) Pizza");
            Console.WriteLine("  2) Bebida");
            Console.WriteLine("  3) Entrada");
            Console.WriteLine("  4) Postre");
            Console.WriteLine("  0) Terminar");
            Console.Write("> ");

            switch (Console.ReadLine())
            {
                case "1": AgregarPizza(pedido); break;
                case "2": AgregarBebida(pedido); break;
                case "3": AgregarEntrada(pedido); break;
                case "4": AgregarPostre(pedido); break;
                case "0": continuar = false; break;
                default: Console.WriteLine("Opción inválida"); break;
            }
        }
    }

    private void AgregarPizza(IPedido pedido)
    {
        Console.WriteLine("\n  Tipo:");
        Console.WriteLine("    1) Normal");
        Console.WriteLine("    2) Estofada (+15%)");
        Console.Write("  > ");
        var tipoPizza = Console.ReadLine();

        Console.WriteLine("\n  Tamaño:");
        Console.WriteLine("    1) Personal  - $15.000");
        Console.WriteLine("    2) Mediana   - $25.000");
        Console.WriteLine("    3) Grande    - $35.000");
        Console.WriteLine("    4) Familiar  - $45.000");
        Console.Write("  > ");
        var tamano = Console.ReadLine() switch
        {
            "1" => "personal", "2" => "mediana", "3" => "grande", "4" => "familiar", _ => "mediana"
        };

        var chef = _franquicia.GetEmpleadosPorRol<Chef>().FirstOrDefault();
        if (chef == null) { Console.WriteLine("  ✗ No hay chef disponible"); return; }

        IPizzaBuilder builder = tipoPizza == "2" ? new PizzaEstofadaBuilder() : new PizzaNormalBuilder();
        Pizza pizza = new DirectorPizza(builder, chef).ConstruirEstandar(tamano);

        Console.WriteLine("\n  ¿Extra?");
        Console.WriteLine("    1) Queso Extra  (+$3.000)");
        Console.WriteLine("    2) Tocineta     (+$4.500)");
        Console.WriteLine("    3) Champiñones  (+$3.500)");
        Console.WriteLine("    0) Sin extras");
        Console.Write("  > ");
        switch (Console.ReadLine())
        {
            case "1": pizza = new QuesoExtra(pizza); break;
            case "2": pizza = new Tocineta(pizza); break;
            case "3": pizza = new Champinones(pizza); break;
        }

        pizza.CambiarEstado(new PizzaRecibida());
        pedido.AgregarItem(pizza, 1);
        Console.WriteLine($"  ✓ {pizza.Nombre} ({tamano}) — ${pizza.CalcularPrecio():N0}");
    }

    private void AgregarBebida(IPedido pedido)
    {
        Console.WriteLine("\n  Bebidas:");
        Console.WriteLine("    1) Gaseosa 350ml  - $4.000");
        Console.WriteLine("    2) Gaseosa 600ml  - $6.000");
        Console.WriteLine("    3) Agua           - $3.000");
        Console.WriteLine("    4) Cerveza        - $7.000");
        Console.Write("  > ");
        Bebida b = Console.ReadLine() switch
        {
            "1" => new Bebida(1, "Gaseosa 350ml", 4000, 350),
            "2" => new Bebida(2, "Gaseosa 600ml", 6000, 600),
            "3" => new Bebida(3, "Agua", 3000, 500),
            "4" => new Bebida(4, "Cerveza", 7000, 330, true),
            _   => new Bebida(1, "Gaseosa 350ml", 4000, 350)
        };
        pedido.AgregarItem(b, 1);
        Console.WriteLine($"  ✓ {b.Nombre} — ${b.CalcularPrecio():N0}");
    }

    private void AgregarEntrada(IPedido pedido)
    {
        Console.WriteLine("\n  Entradas:");
        Console.WriteLine("    1) Palitos de ajo  - $8.000");
        Console.WriteLine("    2) Alitas BBQ      - $18.000");
        Console.WriteLine("    3) Ensalada César  - $12.000");
        Console.Write("  > ");
        Entrada e = Console.ReadLine() switch
        {
            "1" => new Entrada(1, "Palitos de ajo", 8000),
            "2" => new Entrada(2, "Alitas BBQ", 18000),
            "3" => new Entrada(3, "Ensalada César", 12000),
            _   => new Entrada(1, "Palitos de ajo", 8000)
        };
        pedido.AgregarItem(e, 1);
        Console.WriteLine($"  ✓ {e.Nombre} — ${e.CalcularPrecio():N0}");
    }

    private void AgregarPostre(IPedido pedido)
    {
        Console.WriteLine("\n  Postres:");
        Console.WriteLine("    1) Tiramisú  - $12.000");
        Console.WriteLine("    2) Brownie   - $9.000");
        Console.WriteLine("    3) Helado    - $7.000");
        Console.Write("  > ");
        Postre p = Console.ReadLine() switch
        {
            "1" => new Postre(1, "Tiramisú", 12000, "Italiano", 1),
            "2" => new Postre(2, "Brownie", 9000, "Americano", 1),
            "3" => new Postre(3, "Helado", 7000, "Clásico", 1),
            _   => new Postre(1, "Tiramisú", 12000, "Italiano", 1)
        };
        pedido.AgregarItem(p, 1);
        Console.WriteLine($"  ✓ {p.Nombre} — ${p.CalcularPrecio():N0}");
    }

    private void VerPedidos()
    {
        Console.WriteLine("\n=== PEDIDOS ===");
        var pedidos = _pedidoService.GetPedidos();
        if (pedidos.Count == 0) { Console.WriteLine("  No hay pedidos"); return; }
        foreach (var p in pedidos)
            Console.WriteLine($"  Pedido #{p.Id} — ${p.CalcularTotalCliente():N0}");
    }

    private void VerMesas()
    {
        Console.WriteLine("\n=== MESAS ===");
        foreach (var m in _franquicia.GetMesas())
        {
            var info = _mesasAbiertas.TryGetValue(m.Numero, out var ma)
                ? $" — {ma.Cliente.Nombre} (${ma.Pedido.CalcularTotalCliente():N0})"
                : "";
            Console.WriteLine($"  Mesa {m.Numero} (cap. {m.Capacidad}): {m.GetEstado()}{info}");
        }
        if (_franquicia.GetMesas().Count == 0)
            Console.WriteLine("  No hay mesas registradas");
    }

    private void VerCocina()
    {
        Console.WriteLine("\n=== COCINA ===");
        Console.WriteLine($"  Franquicia: {_cocina.Franquicia.Nombre}");
        Console.WriteLine($"  Comandos en historial: {_cocina.GetHistorial().Count}");
        Console.WriteLine("  1. Deshacer último comando");
        Console.WriteLine("  0. Volver");
        Console.Write("> ");
        if (Console.ReadLine() == "1")
            _cocina.DeshacerUltimo();
    }

    private void VerEmpleados()
    {
        Console.WriteLine("\n=== EMPLEADOS ===");
        foreach (var e in _franquicia.GetEmpleados())
            Console.WriteLine($"  {e}");
        if (_franquicia.GetEmpleados().Count == 0)
            Console.WriteLine("  No hay empleados registrados");
    }
}
