using CasoPropuesto_5.Models;

namespace CasoPropuesto_5.Repository;

public interface IMateriasPrimaRepository : IRepositorio<MateriasPrima>
{
    Task<IEnumerable<MateriasPrima>> ObtenerConProveedorAsync();
    Task<IEnumerable<MateriasPrima>> ObtenerConAlertaReabastecimientoAsync();
    Task<IEnumerable<MateriasPrima>> ObtenerPorProveedorAsync(int proveedorId);
}
