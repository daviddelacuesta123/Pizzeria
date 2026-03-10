using System.Linq;

namespace Pizzeria.Domain.Productos
{
    public abstract class Pizza : Producto, IProductoConExtras
    {
        public string Tamano { get; set; }
        public List<Ingrediente> Ingredientes { get; set; } = new();

        public void AgregarIngrediente(Ingrediente ingrediente)
        {
            Ingredientes.Add(ingrediente);
        }

        public List<Ingrediente> ObtenerExtras()
        {
            return Ingredientes.Where(i => i.EsExtra).ToList();
        }

        protected double CalcularExtras()
        {
            return Ingredientes
                .Where(i => i.EsExtra)
                .Sum(i => i.PrecioExtra);
        }
    }
}
