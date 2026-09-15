using CasoPropuesto_5.Models;

namespace CasoPropuesto_5.Repository;

public interface IOrdenesProduccionRepository : IRepositorio<OrdenesProduccion>
{
    Task<OrdenesProduccion?> ObtenerConInspeccionesAsync(int id);
    Task<OrdenesProduccion?> ObtenerPorCodigoAsync(string codigoOrden);
    Task<IEnumerable<OrdenesProduccion>> ObtenerPorEstadoAsync(string estado);
    Task<Dictionary<string, int>> ContarPorEstadoAsync();
}
