namespace Pizzeria.Domain.Organizacion;

using Pizzeria.Domain.Inventario;

public class Franquicia : Pizzeria.Domain.Shared.IAggregateRoot
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public string Direccion { get; private set; }
    public string Ciudad { get; private set; }
    public string Telefono { get; private set; }
    public Empresa Empresa { get; private set; }
    public ConvenioRappi? ConvenioRappi { get; private set; }

    private readonly List<Mesa> _mesas = new();
    private readonly List<Empleado> _empleados = new();
    private readonly List<StockInsumo> _stockInsumos = new();

    public Franquicia(int id, string nombre, string direccion, string ciudad, string telefono, Empresa empresa)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Ciudad = ciudad;
        Telefono = telefono;
        Empresa = empresa;
    }

    public IReadOnlyList<Mesa> GetMesas() => _mesas.AsReadOnly();

    public void AgregarMesa(Mesa mesa)
    {
        if (mesa == null) throw new ArgumentNullException(nameof(mesa));
        _mesas.Add(mesa);
    }

    public List<Mesa> GetMesasDisponibles() => _mesas.Where(m => m.EstaDisponible()).ToList();

    public IReadOnlyList<Empleado> GetEmpleados() => _empleados.AsReadOnly();

    public void AgregarEmpleado(Empleado e)
    {
        if (e == null) throw new ArgumentNullException(nameof(e));
        _empleados.Add(e);
    }

    public List<T> GetEmpleadosPorRol<T>() where T : Empleado => _empleados.OfType<T>().ToList();

    public IReadOnlyList<StockInsumo> GetStockInsumos() => _stockInsumos.AsReadOnly();

    public void AgregarStockInsumo(StockInsumo stock)
    {
        if (stock == null) throw new ArgumentNullException(nameof(stock));
        _stockInsumos.Add(stock);
    }

    public List<StockInsumo> GetStockBajo() => _stockInsumos.Where(s => s.EsBajoStock()).ToList();

    public StockInsumo? BuscarStock(Insumo insumo) =>
        _stockInsumos.FirstOrDefault(s => s.Insumo.Id == insumo.Id);

    public void EstablecerConvenioRappi(ConvenioRappi convenio) => ConvenioRappi = convenio;
}
