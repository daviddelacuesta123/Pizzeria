namespace Pizzeria.Domain.Productos;

public class Estofada : Pizza
{
    public Estofada()
    {
        Nombre = "Pizza Estofada";
    }

    public override double CalcularPrecio() => base.CalcularPrecio() * 1.15;
}
