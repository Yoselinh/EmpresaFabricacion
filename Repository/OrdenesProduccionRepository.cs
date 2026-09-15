using CasoPropuesto_5.Models;
using Microsoft.EntityFrameworkCore;

namespace CasoPropuesto_5.Repository;

public class OrdenesProduccionRepository : Repositorio<OrdenesProduccion>, IOrdenesProduccionRepository
{
    public OrdenesProduccionRepository(AppDbContext contexto) : base(contexto)
    {
    }

    public override async Task<IEnumerable<OrdenesProduccion>> ObtenerTodosAsync()
    {
        return await DbSet
            .Include(o => o.InspeccionesCalidads)
            .AsNoTracking()
            .OrderByDescending(o => o.FechaInicio)
            .ToListAsync();
    }

    public async Task<OrdenesProduccion?> ObtenerConInspeccionesAsync(int id)
    {
        return await DbSet
            .Include(o => o.InspeccionesCalidads)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<OrdenesProduccion?> ObtenerPorCodigoAsync(string codigoOrden)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.CodigoOrden == codigoOrden);
    }

    public async Task<IEnumerable<OrdenesProduccion>> ObtenerPorEstadoAsync(string estado)
    {
        return await DbSet
            .AsNoTracking()
            .Where(o => o.Estado == estado)
            .OrderByDescending(o => o.FechaInicio)
            .ToListAsync();
    }

    public async Task<Dictionary<string, int>> ContarPorEstadoAsync()
    {
        return await DbSet
            .AsNoTracking()
            .GroupBy(o => o.Estado)
            .Select(g => new { Estado = g.Key, Total = g.Count() })
            .ToDictionaryAsync(x => x.Estado, x => x.Total);
    }
}
