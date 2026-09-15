using CasoPropuesto_5.Models;
using Microsoft.EntityFrameworkCore;

namespace CasoPropuesto_5.Repository;

public class InspeccionesCalidadRepository : Repositorio<InspeccionesCalidad>, IInspeccionesCalidadRepository
{
    public InspeccionesCalidadRepository(AppDbContext contexto) : base(contexto)
    {
    }

    public override async Task<IEnumerable<InspeccionesCalidad>> ObtenerTodosAsync()
    {
        return await DbSet
            .Include(i => i.OrdenProduccion)
            .AsNoTracking()
            .OrderByDescending(i => i.FechaInspeccion)
            .ToListAsync();
    }

    public override async Task<InspeccionesCalidad?> ObtenerPorIdAsync(int id)
    {
        return await DbSet
            .Include(i => i.OrdenProduccion)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<InspeccionesCalidad>> ObtenerPorOrdenAsync(int ordenProduccionId)
    {
        return await DbSet
            .AsNoTracking()
            .Where(i => i.OrdenProduccionId == ordenProduccionId)
            .OrderBy(i => i.FechaInspeccion)
            .ToListAsync();
    }

    public async Task<IEnumerable<InspeccionesCalidad>> ObtenerPorEtapaAsync(string etapa)
    {
        return await DbSet
            .Include(i => i.OrdenProduccion)
            .AsNoTracking()
            .Where(i => i.Etapa == etapa)
            .OrderByDescending(i => i.FechaInspeccion)
            .ToListAsync();
    }

    public async Task<IEnumerable<InspeccionesCalidad>> ObtenerDefectuosasAsync()
    {
        return await DbSet
            .Include(i => i.OrdenProduccion)
            .AsNoTracking()
            .Where(i => i.Resultado == "Defectuoso")
            .OrderByDescending(i => i.FechaInspeccion)
            .ToListAsync();
    }

    public async Task<(int Total, int Aprobadas, int Defectuosas)> ObtenerResumenAsync()
    {
        var total = await DbSet.CountAsync();
        var aprobadas = await DbSet.CountAsync(i => i.Resultado == "Aprobado");
        var defectuosas = await DbSet.CountAsync(i => i.Resultado == "Defectuoso");
        return (total, aprobadas, defectuosas);
    }
}
