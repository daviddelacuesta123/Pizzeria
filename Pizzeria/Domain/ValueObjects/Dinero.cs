namespace Pizzeria.Domain.ValueObjects;

public sealed record Dinero
{
    public decimal Monto { get; }
    public string Moneda { get; }

    public Dinero(decimal monto, string moneda = "COP")
    {
        if (monto < 0)
            throw new ArgumentException("El monto no puede ser negativo");
        if (string.IsNullOrWhiteSpace(moneda))
            throw new ArgumentException("La moneda es obligatoria");
        Monto = monto;
        Moneda = moneda.ToUpper();
    }

    public static Dinero Cero(string moneda = "COP") => new(0, moneda);

    public Dinero Sumar(Dinero otro)
    {
        ValidarMismaMoneda(otro);
        return new Dinero(Monto + otro.Monto, Moneda);
    }

    public Dinero Restar(Dinero otro)
    {
        ValidarMismaMoneda(otro);
        return new Dinero(Monto - otro.Monto, Moneda);
    }

    public Dinero Multiplicar(decimal factor)
    {
        if (factor < 0) throw new ArgumentException("El factor no puede ser negativo");
        return new Dinero(Monto * factor, Moneda);
    }

    private void ValidarMismaMoneda(Dinero otro)
    {
        if (Moneda != otro.Moneda)
            throw new InvalidOperationException($"No se pueden operar monedas distintas: {Moneda} vs {otro.Moneda}");
    }

    public override string ToString() => $"${Monto:N0} {Moneda}";
}
