namespace Pizzeria.Eventos;

using Pizzeria.Domain.Inventario;
using Pizzeria.Domain.Organizacion;

public class OrdenAprobadaEvento
{
    public OrdenCompra Orden { get; private set; }
    public Gerente Gerente { get; private set; }

    public OrdenAprobadaEvento(OrdenCompra orden, Gerente gerente)
    {
        Orden = orden;
        Gerente = gerente;
    }

    public OrdenCompra GetOrden() => Orden;
    public Gerente GetGerente() => Gerente;
}
