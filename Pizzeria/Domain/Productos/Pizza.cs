namespace Pizzeria.Domain.Productos;

using Pizzeria.Patrones.States;

public abstract class Pizza : Producto
{
    public string Tamano { get; protected set; } = "";
    protected List<Ingrediente> _ingredientes = new();
    protected IEstadoPizza? _estadoActual;

    public IReadOnlyList<Ingrediente> GetIngredientes() => _ingredientes.AsReadOnly();

    public virtual void AgregarIngrediente(Ingrediente i)
    {
        if (i == null) throw new ArgumentNullException(nameof(i));
        _ingredientes.Add(i);
    }

    public void CambiarEstado(IEstadoPizza estado) => _estadoActual = estado;
    public IEstadoPizza? GetEstado() => _estadoActual;
    public void SetTamano(string tamano) => Tamano = tamano;
    public void SetPrecioBase(double precio) => PrecioBase = precio;
    public void SetIngredientes(List<Ingrediente> ings) => _ingredientes = new List<Ingrediente>(ings);

    public override double CalcularPrecio()
    {
        double total = PrecioBase;
        foreach (var ing in _ingredientes)
            if (ing.EsExtra) total += ing.PrecioExtra;
        return total;
    }
}
