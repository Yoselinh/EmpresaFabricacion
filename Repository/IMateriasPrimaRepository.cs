using CasoPropuesto_5.Models;
using CasoPropuesto_5.Repository.Implements;

namespace CasoPropuesto_5.Repository;

public interface IMateriasPrimaRepository : IGenericRepository<MateriasPrima>
{
    Task<IEnumerable<MateriasPrima>> ObtenerConProveedorAsync();
    Task<IEnumerable<MateriasPrima>> ObtenerConAlertaReabastecimientoAsync();
    Task<IEnumerable<MateriasPrima>> ObtenerPorProveedorAsync(int proveedorId);
}
