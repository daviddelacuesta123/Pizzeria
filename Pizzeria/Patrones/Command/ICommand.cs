namespace Pizzeria.Patrones.Command;

public interface ICommand
{
    void Ejecutar();
    void Deshacer();
}
