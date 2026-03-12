namespace Pizzeria.Domain.Productos
{
    public class Estofada : Pizza
    {
        public double PrecioBase { get; set; }

        public override double CalcularPrecio()
        {
            return PrecioBase + CalcularExtras();
        }
    }
}
