using CasoPropuesto_5.Dtos;
using CasoPropuesto_5.Models;

namespace CasoPropuesto_5.Servicies;

public interface ICalidadService
{
    Task<IEnumerable<InspeccionesCalidad>> ObtenerTodasAsync();
    Task<InspeccionesCalidad?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<InspeccionesCalidad>> ObtenerPorOrdenAsync(int ordenProduccionId);
    Task<IEnumerable<InspeccionesCalidad>> ObtenerPorEtapaAsync(string etapa);
    Task<IEnumerable<InspeccionesCalidad>> ObtenerDefectuosasAsync();
    Task<InspeccionesCalidad> RegistrarAsync(InspeccionCalidadDto dto);
    Task<InspeccionesCalidad> MarcarDefectuosoAsync(int id, string? observaciones);
    Task EliminarAsync(int id);
}
