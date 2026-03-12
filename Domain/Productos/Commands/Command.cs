namespace Pizzeria.Domain.Productos.Commands
{
    public interface Command
    {
        void Ejecutar();
        void Deshacer();
    }
}
