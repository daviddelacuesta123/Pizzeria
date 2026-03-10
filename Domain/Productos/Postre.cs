namespace Pizzeria.Domain.Productos
{
    public class Postre : Producto
    {
        public Postre(string nombre)
        {
            Nombre = nombre;

            Precio = nombre switch
            {
                "Brownie" => 8000,
                "Cheesecake" => 10000,
                _ => 7000
            };
        }

        public double Precio { get; private set; }

        public override double CalcularPrecio() => Precio;
    }
}