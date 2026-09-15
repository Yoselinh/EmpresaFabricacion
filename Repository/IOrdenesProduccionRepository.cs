using CasoPropuesto_5.Models;
using CasoPropuesto_5.Repository.Implements;

namespace CasoPropuesto_5.Repository;

public interface IOrdenesProduccionRepository : IGenericRepository<OrdenesProduccion>
{
    Task<OrdenesProduccion?> ObtenerConInspeccionesAsync(int id);
    Task<OrdenesProduccion?> ObtenerPorCodigoAsync(string codigoOrden);
    Task<IEnumerable<OrdenesProduccion>> ObtenerPorEstadoAsync(string estado);
    Task<Dictionary<string, int>> ContarPorEstadoAsync();
}
