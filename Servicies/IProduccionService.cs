using CasoPropuesto_5.Dtos;
using CasoPropuesto_5.Models;

namespace CasoPropuesto_5.Servicies;

public interface IProduccionService
{
    Task<IEnumerable<OrdenesProduccion>> ObtenerTodasAsync();
    Task<OrdenesProduccion?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<OrdenesProduccion>> ObtenerPorEstadoAsync(string estado);
    Task<OrdenesProduccion> CrearAsync(OrdenProduccionDto dto);
    Task<OrdenesProduccion> ActualizarAsync(int id, OrdenProduccionDto dto);
    Task<OrdenesProduccion> CambiarEstadoAsync(int id, string estado);
    Task EliminarAsync(int id);
}
