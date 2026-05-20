namespace Pizzeria.Repositorios.EnMemoria;

using Pizzeria.Domain.Organizacion;
using Pizzeria.Repositorios.Interfaces;

public class FranquiciaRepositoryEnMemoria : IFranquiciaRepository
{
    private readonly List<Franquicia> _franquicias = new();

    public void Agregar(Franquicia entidad)
    {
        if (entidad == null) throw new ArgumentNullException(nameof(entidad));
        _franquicias.Add(entidad);
    }

    public Franquicia? ObtenerPorId(int id) => _franquicias.FirstOrDefault(f => f.Id == id);

    public IReadOnlyList<Franquicia> ObtenerTodos() => _franquicias.AsReadOnly();

    public IReadOnlyList<Franquicia> ObtenerPorEmpresa(int empresaId)
    {
        return _franquicias
            .Where(f => f.Empresa.Id == empresaId)
            .ToList()
            .AsReadOnly();
    }
}
