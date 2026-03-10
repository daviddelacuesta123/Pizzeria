namespace Pizzeria.Domain.Productos
{
    public class Especial : Pizza
    {
        private double PrecioBase { get; }

        public string Sabor { get; private set; }

        public Especial(string tamano, string sabor)
        {
            Tamano = tamano;
            Sabor = sabor;
            Nombre = $"Pizza Especial - {sabor}";

            PrecioBase = (tamano, sabor) switch
            {
                ("Pequeña", "Mexicana") => 22000,
                ("Mediana", "Mexicana") => 27000,
                ("Grande", "Mexicana") => 35000,

                ("Pequeña", "4 Quesos") => 23000,
                ("Mediana", "4 Quesos") => 28000,
                ("Grande", "4 Quesos") => 36000,

                _ => 25000
            };
        }

        public override double CalcularPrecio()
        {
            return PrecioBase + CalcularExtras();
        }
    }
}
