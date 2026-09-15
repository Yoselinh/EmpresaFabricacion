using CasoPropuesto_5.Models;
using Microsoft.EntityFrameworkCore;

namespace CasoPropuesto_5.Repository;

public class MateriasPrimaRepository : Repositorio<MateriasPrima>, IMateriasPrimaRepository
{
    public MateriasPrimaRepository(AppDbContext contexto) : base(contexto)
    {
    }

    public async Task<IEnumerable<MateriasPrima>> ObtenerConProveedorAsync()
    {
        return await DbSet
            .Include(m => m.Proveedor)
            .AsNoTracking()
            .OrderBy(m => m.Nombre)
            .ToListAsync();
    }

    public override async Task<MateriasPrima?> ObtenerPorIdAsync(int id)
    {
        return await DbSet
            .Include(m => m.Proveedor)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<MateriasPrima>> ObtenerConAlertaReabastecimientoAsync()
    {
        return await DbSet
            .Include(m => m.Proveedor)
            .AsNoTracking()
            .Where(m => m.StockActual <= m.StockMinimo)
            .OrderBy(m => m.StockActual)
            .ToListAsync();
    }

    public async Task<IEnumerable<MateriasPrima>> ObtenerPorProveedorAsync(int proveedorId)
    {
        return await DbSet
            .AsNoTracking()
            .Where(m => m.ProveedorId == proveedorId)
            .ToListAsync();
    }
}
