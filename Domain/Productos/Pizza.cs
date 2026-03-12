using System.Linq;

namespace Pizzeria.Domain.Productos
{
    public abstract class Pizza : Producto, IProductoConExtras
    {
        public string Tamano { get; set; }
        public List<Ingrediente> Ingredientes { get; set; } = new();
        
        private Estados.EstadoPizza _estadoActual;

        public Pizza()
        {
            _estadoActual = new Estados.PedidoRecibido();
        }

        public void CambiarEstado(Estados.EstadoPizza estado)
        {
            _estadoActual = estado;
        }

        public Estados.EstadoPizza GetEstado()
        {
            return _estadoActual;
        }

        public void ManejarEstado()
        {
            _estadoActual.ManejarEstado(this);
        }

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
