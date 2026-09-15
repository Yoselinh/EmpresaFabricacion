using CasoPropuesto_5.Models;

namespace CasoPropuesto_5.Repository;

public interface IInspeccionesCalidadRepository : IRepositorio<InspeccionesCalidad>
{
    Task<IEnumerable<InspeccionesCalidad>> ObtenerPorOrdenAsync(int ordenProduccionId);
    Task<IEnumerable<InspeccionesCalidad>> ObtenerPorEtapaAsync(string etapa);
    Task<IEnumerable<InspeccionesCalidad>> ObtenerDefectuosasAsync();
    Task<(int Total, int Aprobadas, int Defectuosas)> ObtenerResumenAsync();
}
