namespace Pizzeria.Domain.Productos
{
    public abstract class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public abstract double CalcularPrecio();
    }
}
