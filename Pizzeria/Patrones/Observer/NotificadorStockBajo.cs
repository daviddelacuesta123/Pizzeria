namespace Pizzeria.Patrones.Observer;

using Pizzeria.Eventos;
using Pizzeria.Domain.Organizacion;

public class NotificadorStockBajo : IObservador<StockBajoEvento>
{
    public void Actualizar(StockBajoEvento evento)
    {
        Console.WriteLine($"[ALERTA STOCK] Franquicia {evento.Franquicia.Nombre}: {evento.Insumo.Nombre} con {evento.CantidadActual} {evento.Insumo.UnidadMedida}");
        var gerentes = evento.Franquicia.GetEmpleadosPorRol<Gerente>();
        foreach (var g in gerentes)
            AlertarGerente(g, evento);
    }

    public void AlertarGerente(Gerente gerente, StockBajoEvento evento)
    {
        Console.WriteLine($"  → Gerente {gerente.Nombre} notificado sobre stock bajo de {evento.Insumo.Nombre}");
    }
}
