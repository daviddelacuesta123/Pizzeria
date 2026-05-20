namespace Pizzeria.Domain.ValueObjects;

public sealed record Nit
{
    public string Valor { get; }

    public Nit(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("El NIT es obligatorio");
        var limpio = valor.Trim().Replace(".", "").Replace("-", "");
        if (limpio.Length < 8 || limpio.Length > 11)
            throw new ArgumentException($"El NIT debe tener entre 8 y 11 dígitos, recibido: {valor}");
        if (!limpio.All(char.IsDigit))
            throw new ArgumentException($"El NIT solo puede contener dígitos, guiones o puntos: {valor}");
        Valor = valor.Trim();
    }

    public override string ToString() => Valor;

    public static implicit operator string(Nit nit) => nit.Valor;
}
