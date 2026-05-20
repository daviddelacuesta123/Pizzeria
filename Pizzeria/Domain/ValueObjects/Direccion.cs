namespace Pizzeria.Domain.ValueObjects;

public sealed record Direccion
{
    public string Calle { get; }
    public string Ciudad { get; }
    public string? Referencia { get; }

    public Direccion(string calle, string ciudad, string? referencia = null)
    {
        if (string.IsNullOrWhiteSpace(calle))
            throw new ArgumentException("La calle es obligatoria");
        if (string.IsNullOrWhiteSpace(ciudad))
            throw new ArgumentException("La ciudad es obligatoria");
        Calle = calle.Trim();
        Ciudad = ciudad.Trim();
        Referencia = referencia?.Trim();
    }

    public override string ToString() =>
        string.IsNullOrEmpty(Referencia)
            ? $"{Calle}, {Ciudad}"
            : $"{Calle}, {Ciudad} ({Referencia})";
}
