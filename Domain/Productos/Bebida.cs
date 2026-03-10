namespace Pizzeria.Domain.Productos
{
     public class Bebida : Producto
    {
        public Bebida(string nombre)
        {
            Nombre = nombre;

            Precio = nombre switch
            {
                "Gaseosa" => 5000,
                "Jugo Natural" => 7000,
                "Cerveza" => 9000,
                _ => 5000
            };
        }

        public double Precio { get; private set; }

        public override double CalcularPrecio() => Precio;
    }
}
