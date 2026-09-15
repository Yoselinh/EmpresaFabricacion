using CasoPropuesto_5.Models;

namespace CasoPropuesto_5.Repository;

public interface IProveedorRepository : IRepositorio<Proveedore>
{
    Task<Proveedore?> ObtenerConMateriasPrimasAsync(int id);
    Task<IEnumerable<Proveedore>> ObtenerConEvaluacionAsync();
    Task<decimal?> ObtenerPromedioEvaluacionAsync();
}
