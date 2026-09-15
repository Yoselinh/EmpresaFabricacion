using CasoPropuesto_5.Dtos;
using CasoPropuesto_5.Models;

namespace CasoPropuesto_5.Servicies;

public interface IInventarioService
{
    Task<IEnumerable<MateriasPrima>> ObtenerTodasAsync();
    Task<MateriasPrima?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<MateriasPrima>> ObtenerAlertasAsync();
    Task<MateriasPrima> CrearAsync(MateriaPrimaDto dto);
    Task<MateriasPrima> ActualizarAsync(int id, MateriaPrimaDto dto);
    Task<MateriasPrima> ReabastecerAsync(int id, int cantidad);
    Task EliminarAsync(int id);
}
