using CasoPropuesto_5.Dtos;
using CasoPropuesto_5.Models;
using CasoPropuesto_5.Repository;

namespace CasoPropuesto_5.Servicies;

public class InventarioService : IInventarioService
{
    private readonly IMateriasPrimaRepository _repositorio;
    private readonly IProveedorRepository _proveedores;

    public InventarioService(
        IMateriasPrimaRepository repositorio,
        IProveedorRepository proveedores)
    {
        _repositorio = repositorio;
        _proveedores = proveedores;
    }

    public Task<IEnumerable<MateriasPrima>> ObtenerTodasAsync()
    {
        return _repositorio.ObtenerConProveedorAsync();
    }

    public Task<MateriasPrima?> ObtenerPorIdAsync(int id)
    {
        return _repositorio.ObtenerPorIdAsync(id);
    }

    public Task<IEnumerable<MateriasPrima>> ObtenerAlertasAsync()
    {
        return _repositorio.ObtenerConAlertaReabastecimientoAsync();
    }

    public async Task<MateriasPrima> CrearAsync(MateriaPrimaDto dto)
    {
        ValidarStock(dto.StockActual, dto.StockMinimo);
        await ValidarProveedorAsync(dto.ProveedorId);

        var materia = new MateriasPrima
        {
            Nombre = dto.Nombre.Trim(),
            StockActual = dto.StockActual,
            StockMinimo = dto.StockMinimo <= 0 ? 10 : dto.StockMinimo,
            ProveedorId = dto.ProveedorId
        };

        return await _repositorio.CrearAsync(materia);
    }

    public async Task<MateriasPrima> ActualizarAsync(int id, MateriaPrimaDto dto)
    {
        var materia = await _repositorio.ObtenerPorIdAsync(id)
                      ?? throw new KeyNotFoundException($"No existe la materia prima {id}.");

        ValidarStock(dto.StockActual, dto.StockMinimo);
        await ValidarProveedorAsync(dto.ProveedorId);

        materia.Nombre = dto.Nombre.Trim();
        materia.StockActual = dto.StockActual;
        materia.StockMinimo = dto.StockMinimo <= 0 ? 10 : dto.StockMinimo;
        materia.ProveedorId = dto.ProveedorId;

        await _repositorio.ActualizarAsync(materia);
        return materia;
    }

    public async Task<MateriasPrima> ReabastecerAsync(int id, int cantidad)
    {
        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad a reabastecer debe ser mayor a 0.");
        }

        var materia = await _repositorio.ObtenerPorIdAsync(id)
                      ?? throw new KeyNotFoundException($"No existe la materia prima {id}.");

        materia.StockActual += cantidad;
        await _repositorio.ActualizarAsync(materia);
        return materia;
    }

    public async Task EliminarAsync(int id)
    {
        var materia = await _repositorio.ObtenerPorIdAsync(id)
                      ?? throw new KeyNotFoundException($"No existe la materia prima {id}.");

        await _repositorio.EliminarAsync(materia);
    }

    private static void ValidarStock(int stockActual, int stockMinimo)
    {
        if (stockActual < 0)
        {
            throw new ArgumentException("El stock actual no puede ser negativo.");
        }

        if (stockMinimo < 0)
        {
            throw new ArgumentException("El stock mínimo no puede ser negativo.");
        }
    }

    private async Task ValidarProveedorAsync(int? proveedorId)
    {
        if (proveedorId is null)
        {
            return;
        }

        if (!await _proveedores.ExisteAsync(proveedorId.Value))
        {
            throw new KeyNotFoundException($"No existe el proveedor {proveedorId}.");
        }
    }
}
