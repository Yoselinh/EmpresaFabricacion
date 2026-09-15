using CasoPropuesto_5.Dtos;
using CasoPropuesto_5.Models;
using CasoPropuesto_5.Repository;

namespace CasoPropuesto_5.Servicies;

public class ProduccionService : IProduccionService
{
    private static readonly string[] EstadosCiclo =
    [
        "Planificada",
        "En proceso",
        "En calidad",
        "Terminada",
        "Entregada",
        "Cancelada"
    ];

    private readonly IOrdenesProduccionRepository _repositorio;

    public ProduccionService(IOrdenesProduccionRepository repositorio)
    {
        _repositorio = repositorio;
    }

    public Task<IEnumerable<OrdenesProduccion>> ObtenerTodasAsync()
    {
        return _repositorio.ObtenerTodosAsync();
    }

    public async Task<OrdenesProduccion?> ObtenerPorIdAsync(int id)
    {
        return await _repositorio.ObtenerConInspeccionesAsync(id);
    }

    public Task<IEnumerable<OrdenesProduccion>> ObtenerPorEstadoAsync(string estado)
    {
        return _repositorio.ObtenerPorEstadoAsync(estado);
    }

    public async Task<OrdenesProduccion> CrearAsync(OrdenProduccionDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.CodigoOrden))
        {
            throw new ArgumentException("El código de orden es obligatorio.");
        }

        var existente = await _repositorio.ObtenerPorCodigoAsync(dto.CodigoOrden);
        if (existente is not null)
        {
            throw new InvalidOperationException($"Ya existe una orden con el código {dto.CodigoOrden}.");
        }

        var estado = NormalizarEstado(string.IsNullOrWhiteSpace(dto.Estado) ? "Planificada" : dto.Estado);

        var orden = new OrdenesProduccion
        {
            CodigoOrden = dto.CodigoOrden.Trim(),
            FechaInicio = dto.FechaInicio ?? DateTime.Now,
            FechaFin = dto.FechaFin,
            Estado = estado
        };

        return await _repositorio.CrearAsync(orden);
    }

    public async Task<OrdenesProduccion> ActualizarAsync(int id, OrdenProduccionDto dto)
    {
        var orden = await _repositorio.ObtenerPorIdAsync(id)
                    ?? throw new KeyNotFoundException($"No existe la orden {id}.");

        if (!string.IsNullOrWhiteSpace(dto.CodigoOrden) && dto.CodigoOrden != orden.CodigoOrden)
        {
            var existente = await _repositorio.ObtenerPorCodigoAsync(dto.CodigoOrden);
            if (existente is not null)
            {
                throw new InvalidOperationException($"Ya existe una orden con el código {dto.CodigoOrden}.");
            }

            orden.CodigoOrden = dto.CodigoOrden.Trim();
        }

        if (!string.IsNullOrWhiteSpace(dto.Estado))
        {
            orden.Estado = NormalizarEstado(dto.Estado);
        }

        if (dto.FechaInicio.HasValue)
        {
            orden.FechaInicio = dto.FechaInicio;
        }

        orden.FechaFin = dto.FechaFin;
        if (orden.Estado is "Terminada" or "Entregada")
        {
            orden.FechaFin ??= DateTime.Now;
        }

        await _repositorio.ActualizarAsync(orden);
        return orden;
    }

    public async Task<OrdenesProduccion> CambiarEstadoAsync(int id, string estado)
    {
        var orden = await _repositorio.ObtenerPorIdAsync(id)
                    ?? throw new KeyNotFoundException($"No existe la orden {id}.");

        orden.Estado = NormalizarEstado(estado);

        if (orden.Estado is "Terminada" or "Entregada")
        {
            orden.FechaFin ??= DateTime.Now;
        }

        await _repositorio.ActualizarAsync(orden);
        return orden;
    }

    public async Task EliminarAsync(int id)
    {
        var orden = await _repositorio.ObtenerPorIdAsync(id)
                    ?? throw new KeyNotFoundException($"No existe la orden {id}.");

        await _repositorio.EliminarAsync(orden);
    }

    private static string NormalizarEstado(string estado)
    {
        var normalizado = EstadosCiclo.FirstOrDefault(e =>
            e.Equals(estado.Trim(), StringComparison.OrdinalIgnoreCase));

        if (normalizado is null)
        {
            throw new ArgumentException(
                $"Estado inválido. Use: {string.Join(", ", EstadosCiclo)}.");
        }

        return normalizado;
    }
}
