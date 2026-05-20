namespace Pizzeria.Domain.Inventario;

public class Distribuidor
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public string Telefono { get; private set; }
    public string Email { get; private set; }

    private readonly List<PrecioMayorista> _catalogoPrecios = new();

    public Distribuidor(int id, string nombre, string telefono, string email)
    {
        Id = id;
        Nombre = nombre;
        Telefono = telefono;
        Email = email;
    }

    public IReadOnlyList<PrecioMayorista> GetCatalogo() => _catalogoPrecios.AsReadOnly();

    public void AgregarPrecio(PrecioMayorista precio)
    {
        if (precio == null) throw new ArgumentNullException(nameof(precio));
        _catalogoPrecios.Add(precio);
    }

    public double GetPrecioMayorista(Insumo insumo)
    {
        var precio = _catalogoPrecios.FirstOrDefault(p => p.Insumo.Id == insumo.Id && p.EstaVigente());
        return precio?.GetPrecio() ?? 0;
    }

    public PrecioMayorista? GetPrecioVigente(Insumo insumo) =>
        _catalogoPrecios.FirstOrDefault(p => p.Insumo.Id == insumo.Id && p.EstaVigente());
}
