using CasoPropuesto_5.Dtos;
using CasoPropuesto_5.Models;
using CasoPropuesto_5.Repository;

namespace CasoPropuesto_5.Servicies;

public class ProveedorService : IProveedorService
{
    private readonly IProveedorRepository _repositorio;

    public ProveedorService(IProveedorRepository repositorio)
    {
        _repositorio = repositorio;
    }

    public Task<IEnumerable<Proveedore>> ObtenerTodosAsync()
    {
        return _repositorio.ObtenerTodosAsync();
    }

    public Task<Proveedore?> ObtenerPorIdAsync(int id)
    {
        return _repositorio.ObtenerConMateriasPrimasAsync(id);
    }

    public async Task<Proveedore> CrearAsync(ProveedorDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nombre))
        {
            throw new ArgumentException("El nombre del proveedor es obligatorio.");
        }

        ValidarEvaluacion(dto.EvaluacionDesempenio);

        var proveedor = new Proveedore
        {
            Nombre = dto.Nombre.Trim(),
            Contacto = dto.Contacto?.Trim(),
            EvaluacionDesempenio = dto.EvaluacionDesempenio
        };

        return await _repositorio.CrearAsync(proveedor);
    }

    public async Task<Proveedore> ActualizarAsync(int id, ProveedorDto dto)
    {
        var proveedor = await _repositorio.ObtenerPorIdAsync(id)
                        ?? throw new KeyNotFoundException($"No existe el proveedor {id}.");

        if (string.IsNullOrWhiteSpace(dto.Nombre))
        {
            throw new ArgumentException("El nombre del proveedor es obligatorio.");
        }

        ValidarEvaluacion(dto.EvaluacionDesempenio);

        proveedor.Nombre = dto.Nombre.Trim();
        proveedor.Contacto = dto.Contacto?.Trim();
        proveedor.EvaluacionDesempenio = dto.EvaluacionDesempenio;

        await _repositorio.ActualizarAsync(proveedor);
        return proveedor;
    }

    public async Task<Proveedore> EvaluarAsync(int id, decimal evaluacion)
    {
        ValidarEvaluacion(evaluacion);

        var proveedor = await _repositorio.ObtenerPorIdAsync(id)
                        ?? throw new KeyNotFoundException($"No existe el proveedor {id}.");

        proveedor.EvaluacionDesempenio = evaluacion;
        await _repositorio.ActualizarAsync(proveedor);
        return proveedor;
    }

    public async Task EliminarAsync(int id)
    {
        var proveedor = await _repositorio.ObtenerPorIdAsync(id)
                        ?? throw new KeyNotFoundException($"No existe el proveedor {id}.");

        await _repositorio.EliminarAsync(proveedor);
    }

    private static void ValidarEvaluacion(decimal? evaluacion)
    {
        if (evaluacion is null)
        {
            return;
        }

        if (evaluacion is < 0 or > 5)
        {
            throw new ArgumentException("La evaluación de desempeño debe estar entre 0 y 5.");
        }
    }
}
