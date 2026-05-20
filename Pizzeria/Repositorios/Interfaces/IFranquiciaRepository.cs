namespace Pizzeria.Repositorios.Interfaces;

using Pizzeria.Domain.Organizacion;

public interface IFranquiciaRepository : IRepository<Franquicia>
{
    IReadOnlyList<Franquicia> ObtenerPorEmpresa(int empresaId);
}
