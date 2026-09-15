using CasoPropuesto_5.Models;
using CasoPropuesto_5.Repository.Implements;

namespace CasoPropuesto_5.Repository;

public interface IProveedorRepository : IGenericRepository<Proveedore>
{
    Task<Proveedore?> ObtenerConMateriasPrimasAsync(int id);
    Task<IEnumerable<Proveedore>> ObtenerConEvaluacionAsync();
    Task<decimal?> ObtenerPromedioEvaluacionAsync();
}
