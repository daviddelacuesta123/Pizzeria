namespace Pizzeria.Domain.Shared;

/// <summary>
/// Marker interface para identificar las raíces de agregado en el dominio (DDD).
/// Un Aggregate Root es la única entrada al agregado: cualquier modificación
/// a sus entidades internas debe pasar por él, garantizando invariantes y consistencia.
/// </summary>
public interface IAggregateRoot
{
}
