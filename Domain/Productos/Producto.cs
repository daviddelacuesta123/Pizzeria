namespace Pizzeria.Domain.Productos
{
    public abstract class Producto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public abstract double CalcularPrecio();
    }
}
