namespace Pizzeria.Domain.Productos
{
    public class Estofada : Pizza
    {
        public double PrecioBase { get; set; }

        public Estofada() { }

        public Estofada(string tamano)
        {
            Tamano = tamano;
            Nombre = "Pizza Estofada";

            PrecioBase = tamano switch
            {
                "Pequeña" => 25000,
                "Mediana" => 32000,
                "Grande" => 40000,
                _ => 32000
            };
        }

        public override double CalcularPrecio()
        {
            return PrecioBase + CalcularExtras();
        }
    }
}
