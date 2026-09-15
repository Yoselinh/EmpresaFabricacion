using CasoPropuesto_5.Dtos;
using CasoPropuesto_5.Models;

namespace CasoPropuesto_5.Servicies;

public interface IProveedorService
{
    Task<IEnumerable<Proveedore>> ObtenerTodosAsync();
    Task<Proveedore?> ObtenerPorIdAsync(int id);
    Task<Proveedore> CrearAsync(ProveedorDto dto);
    Task<Proveedore> ActualizarAsync(int id, ProveedorDto dto);
    Task<Proveedore> EvaluarAsync(int id, decimal evaluacion);
    Task EliminarAsync(int id);
}
