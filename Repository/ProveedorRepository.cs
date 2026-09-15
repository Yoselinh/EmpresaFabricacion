using CasoPropuesto_5.Models;
using Microsoft.EntityFrameworkCore;

namespace CasoPropuesto_5.Repository;

public class ProveedorRepository : Repositorio<Proveedore>, IProveedorRepository
{
    public ProveedorRepository(AppDbContext contexto) : base(contexto)
    {
    }

    public override async Task<IEnumerable<Proveedore>> ObtenerTodosAsync()
    {
        return await DbSet
            .Include(p => p.MateriasPrimas)
            .AsNoTracking()
            .OrderBy(p => p.Nombre)
            .ToListAsync();
    }

    public async Task<Proveedore?> ObtenerConMateriasPrimasAsync(int id)
    {
        return await DbSet
            .Include(p => p.MateriasPrimas)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Proveedore>> ObtenerConEvaluacionAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Where(p => p.EvaluacionDesempenio != null)
            .OrderByDescending(p => p.EvaluacionDesempenio)
            .ToListAsync();
    }

    public async Task<decimal?> ObtenerPromedioEvaluacionAsync()
    {
        if (!await DbSet.AnyAsync(p => p.EvaluacionDesempenio != null))
        {
            return null;
        }

        return await DbSet
            .Where(p => p.EvaluacionDesempenio != null)
            .AverageAsync(p => p.EvaluacionDesempenio!.Value);
    }
}
