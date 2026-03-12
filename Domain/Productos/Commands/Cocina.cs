using System;
using System.Collections.Generic;

namespace Pizzeria.Domain.Productos.Commands
{
    public class Cocina
    {
        private List<Command> _historialComandos = new List<Command>();

        public void EjecutarComando(Command cmd)
        {
            cmd.Ejecutar();
            _historialComandos.Add(cmd);
        }

        public void DeshacerUltimo()
        {
            if (_historialComandos.Count > 0)
            {
                var ultimo = _historialComandos[_historialComandos.Count - 1];
                ultimo.Deshacer();
                _historialComandos.RemoveAt(_historialComandos.Count - 1);
            }
        }

        public void PrepararPizza(Pizza pizza)
        {
            Console.WriteLine($"Preparando la pizza {pizza.Nombre} de tamano {pizza.Tamano}");
            pizza.CambiarEstado(new Estados.EnPreparacion());
        }
    }
}
