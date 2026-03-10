namespace Pizzeria.Domain.Productos
{
    public class Entrada : Producto
    {
        public Entrada(string nombre)
        {
            Nombre = nombre;

            Precio = nombre switch
            {
                "Pan de Ajo" => 8000,
                "Nachos" => 12000,
                _ => 10000
            };
        }

        public double Precio { get; private set; }

        public override double CalcularPrecio() => Precio;
    }
}