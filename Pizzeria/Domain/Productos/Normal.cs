namespace Pizzeria.Domain.Productos;

public class Normal : Pizza
{
    public Normal()
    {
        Nombre = "Pizza Normal";
    }

    public override double CalcularPrecio() => base.CalcularPrecio();
}
