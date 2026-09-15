using CasoPropuesto_5.Dtos;

namespace CasoPropuesto_5.Servicies;

public interface IInformeService
{
    Task<InformeGeneralDto> ObtenerInformeGeneralAsync();
    Task<InformeProduccionDto> ObtenerInformeProduccionAsync();
    Task<InformeCalidadDto> ObtenerInformeCalidadAsync();
    Task<InformeInventarioDto> ObtenerInformeInventarioAsync();
    Task<InformeProveedoresDto> ObtenerInformeProveedoresAsync();
}
