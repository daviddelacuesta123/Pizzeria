namespace Pizzeria.Domain.Productos
{
  public class Normal : Pizza
    {
        private double PrecioBase { get; }

        public string Sabor { get; private set; }

        public Normal(string tamano, string sabor)
        {
            Tamano = tamano;
            Sabor = sabor;
            Nombre = $"Pizza Normal - {sabor}";

            PrecioBase = (tamano, sabor) switch
            {
                ("Pequeña", "Hawaiana") => 16000,
                ("Mediana", "Hawaiana") => 21000,
                ("Grande", "Hawaiana") => 29000,

                ("Pequeña", "Pepperoni") => 17000,
                ("Mediana", "Pepperoni") => 22000,
                ("Grande", "Pepperoni") => 30000,

                _ => 20000
            };
        }

        public override double CalcularPrecio()
        {
            return PrecioBase + CalcularExtras();
        }
    }
}