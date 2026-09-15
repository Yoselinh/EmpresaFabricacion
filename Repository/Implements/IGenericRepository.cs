using System.Linq.Expressions;

namespace CasoPropuesto_5.Repository.Implements;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> ObtenerTodosAsync();
    Task<T?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> predicado);
    Task<T> CrearAsync(T entidad);
    Task ActualizarAsync(T entidad);
    Task EliminarAsync(T entidad);
    Task<bool> ExisteAsync(int id);
    Task GuardarCambiosAsync();
}
