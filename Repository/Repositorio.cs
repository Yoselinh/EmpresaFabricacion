using System.Linq.Expressions;
using CasoPropuesto_5.Models;
using Microsoft.EntityFrameworkCore;

namespace CasoPropuesto_5.Repository;

public class Repositorio<T> : IRepositorio<T> where T : class
{
    protected readonly AppDbContext Contexto;
    protected readonly DbSet<T> DbSet;

    public Repositorio(AppDbContext contexto)
    {
        Contexto = contexto;
        DbSet = contexto.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> ObtenerTodosAsync()
    {
        return await DbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task<T?> ObtenerPorIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> predicado)
    {
        return await DbSet.AsNoTracking().Where(predicado).ToListAsync();
    }

    public virtual async Task<T> CrearAsync(T entidad)
    {
        await DbSet.AddAsync(entidad);
        await Contexto.SaveChangesAsync();
        return entidad;
    }

    public virtual async Task ActualizarAsync(T entidad)
    {
        DbSet.Update(entidad);
        await Contexto.SaveChangesAsync();
    }

    public virtual async Task EliminarAsync(T entidad)
    {
        DbSet.Remove(entidad);
        await Contexto.SaveChangesAsync();
    }

    public virtual async Task<bool> ExisteAsync(int id)
    {
        var entidad = await DbSet.FindAsync(id);
        if (entidad is null)
        {
            return false;
        }

        Contexto.Entry(entidad).State = EntityState.Detached;
        return true;
    }

    public virtual async Task GuardarCambiosAsync()
    {
        await Contexto.SaveChangesAsync();
    }
}
