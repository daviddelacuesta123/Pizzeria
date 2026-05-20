namespace Pizzeria.Domain.Inventario;

using Pizzeria.Domain.Organizacion;

public class Mesa
{
    public int Id { get; private set; }
    public int Numero { get; private set; }
    public int Capacidad { get; private set; }
    public EstadoMesa Estado { get; private set; }
    public Franquicia Franquicia { get; private set; }

    public Mesa(int id, int numero, int capacidad, Franquicia franquicia)
    {
        Id = id;
        Numero = numero;
        Capacidad = capacidad;
        Franquicia = franquicia;
        Estado = EstadoMesa.DISPONIBLE;
    }

    public void Ocupar()
    {
        if (Estado != EstadoMesa.DISPONIBLE)
            throw new InvalidOperationException($"Mesa {Numero} no está disponible (estado: {Estado})");
        Estado = EstadoMesa.OCUPADA;
    }

    public void Liberar() => Estado = EstadoMesa.DISPONIBLE;

    public void Reservar()
    {
        if (Estado != EstadoMesa.DISPONIBLE)
            throw new InvalidOperationException($"Mesa {Numero} no se puede reservar (estado: {Estado})");
        Estado = EstadoMesa.RESERVADA;
    }

    public void PonerFueraDeServicio() => Estado = EstadoMesa.FUERA_SERVICIO;
    public bool EstaDisponible() => Estado == EstadoMesa.DISPONIBLE;
    public EstadoMesa GetEstado() => Estado;
}
