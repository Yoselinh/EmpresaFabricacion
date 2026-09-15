using CasoPropuesto_5.Dtos;
using CasoPropuesto_5.Models;
using CasoPropuesto_5.Repository;

namespace CasoPropuesto_5.Servicies;

public class CalidadService : ICalidadService
{
    private static readonly string[] ResultadosValidos = ["Aprobado", "Defectuoso", "Observado"];

    private readonly IInspeccionesCalidadRepository _repositorio;
    private readonly IOrdenesProduccionRepository _ordenes;

    public CalidadService(
        IInspeccionesCalidadRepository repositorio,
        IOrdenesProduccionRepository ordenes)
    {
        _repositorio = repositorio;
        _ordenes = ordenes;
    }

    public Task<IEnumerable<InspeccionesCalidad>> ObtenerTodasAsync()
    {
        return _repositorio.ObtenerTodosAsync();
    }

    public Task<InspeccionesCalidad?> ObtenerPorIdAsync(int id)
    {
        return _repositorio.ObtenerPorIdAsync(id);
    }

    public Task<IEnumerable<InspeccionesCalidad>> ObtenerPorOrdenAsync(int ordenProduccionId)
    {
        return _repositorio.ObtenerPorOrdenAsync(ordenProduccionId);
    }

    public Task<IEnumerable<InspeccionesCalidad>> ObtenerPorEtapaAsync(string etapa)
    {
        return _repositorio.ObtenerPorEtapaAsync(etapa);
    }

    public Task<IEnumerable<InspeccionesCalidad>> ObtenerDefectuosasAsync()
    {
        return _repositorio.ObtenerDefectuosasAsync();
    }

    public async Task<InspeccionesCalidad> RegistrarAsync(InspeccionCalidadDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Etapa))
        {
            throw new ArgumentException("La etapa de inspección es obligatoria.");
        }

        var orden = await _ordenes.ObtenerPorIdAsync(dto.OrdenProduccionId);
        if (orden is null)
        {
            throw new KeyNotFoundException($"No existe la orden de producción {dto.OrdenProduccionId}.");
        }

        var resultado = string.IsNullOrWhiteSpace(dto.Resultado) ? "Aprobado" : dto.Resultado.Trim();
        ValidarResultado(resultado);

        var inspeccion = new InspeccionesCalidad
        {
            OrdenProduccionId = dto.OrdenProduccionId,
            Etapa = dto.Etapa.Trim(),
            Resultado = resultado,
            Observaciones = dto.Observaciones,
            FechaInspeccion = DateTime.Now
        };

        var creada = await _repositorio.CrearAsync(inspeccion);

        if (resultado == "Defectuoso" && orden.Estado is not "Cancelada" and not "Entregada")
        {
            orden.Estado = "En calidad";
            await _ordenes.ActualizarAsync(orden);
        }

        return creada;
    }

    public async Task<InspeccionesCalidad> MarcarDefectuosoAsync(int id, string? observaciones)
    {
        var inspeccion = await _repositorio.ObtenerPorIdAsync(id)
                         ?? throw new KeyNotFoundException($"No existe la inspección {id}.");

        inspeccion.Resultado = "Defectuoso";
        if (!string.IsNullOrWhiteSpace(observaciones))
        {
            inspeccion.Observaciones = observaciones;
        }

        await _repositorio.ActualizarAsync(inspeccion);

        var orden = await _ordenes.ObtenerPorIdAsync(inspeccion.OrdenProduccionId);
        if (orden is not null && orden.Estado is not "Cancelada" and not "Entregada")
        {
            orden.Estado = "En calidad";
            await _ordenes.ActualizarAsync(orden);
        }

        return inspeccion;
    }

    public async Task EliminarAsync(int id)
    {
        var inspeccion = await _repositorio.ObtenerPorIdAsync(id)
                         ?? throw new KeyNotFoundException($"No existe la inspección {id}.");

        await _repositorio.EliminarAsync(inspeccion);
    }

    private static void ValidarResultado(string resultado)
    {
        if (!ResultadosValidos.Contains(resultado))
        {
            throw new ArgumentException(
                $"Resultado inválido. Use: {string.Join(", ", ResultadosValidos)}.");
        }
    }
}
