namespace Pizzeria.Domain.ValueObjects;

public sealed record Telefono
{
    public string Valor { get; }

    public Telefono(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("El teléfono es obligatorio");
        var limpio = valor.Trim().Replace(" ", "").Replace("-", "").Replace("+", "");
        if (limpio.Length < 7 || limpio.Length > 15)
            throw new ArgumentException($"El teléfono debe tener entre 7 y 15 dígitos, recibido: {valor}");
        if (!limpio.All(char.IsDigit))
            throw new ArgumentException($"El teléfono solo puede contener dígitos: {valor}");
        Valor = valor.Trim();
    }

    public override string ToString() => Valor;

    public static implicit operator string(Telefono t) => t.Valor;
}
