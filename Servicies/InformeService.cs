using CasoPropuesto_5.Dtos;
using CasoPropuesto_5.Repository;

namespace CasoPropuesto_5.Servicies;

public class InformeService : IInformeService
{
    private const decimal CostoUnitarioDefecto = 150m;

    private readonly IOrdenesProduccionRepository _ordenes;
    private readonly IInspeccionesCalidadRepository _inspecciones;
    private readonly IMateriasPrimaRepository _inventario;
    private readonly IProveedorRepository _proveedores;

    public InformeService(
        IOrdenesProduccionRepository ordenes,
        IInspeccionesCalidadRepository inspecciones,
        IMateriasPrimaRepository inventario,
        IProveedorRepository proveedores)
    {
        _ordenes = ordenes;
        _inspecciones = inspecciones;
        _inventario = inventario;
        _proveedores = proveedores;
    }

    public async Task<InformeGeneralDto> ObtenerInformeGeneralAsync()
    {
        return new InformeGeneralDto
        {
            Produccion = await ObtenerInformeProduccionAsync(),
            Calidad = await ObtenerInformeCalidadAsync(),
            Inventario = await ObtenerInformeInventarioAsync(),
            Proveedores = await ObtenerInformeProveedoresAsync()
        };
    }

    public async Task<InformeProduccionDto> ObtenerInformeProduccionAsync()
    {
        var ordenes = (await _ordenes.ObtenerTodosAsync()).ToList();
        var porEstado = await _ordenes.ContarPorEstadoAsync();

        var ciclosCompletos = ordenes
            .Where(o => o.FechaInicio.HasValue && o.FechaFin.HasValue)
            .Select(o => (o.FechaFin!.Value - o.FechaInicio!.Value).TotalDays)
            .ToList();

        return new InformeProduccionDto
        {
            TotalOrdenes = ordenes.Count,
            OrdenesPorEstado = porEstado,
            OrdenesEntregadas = porEstado.GetValueOrDefault("Entregada"),
            OrdenesEnProceso = porEstado.GetValueOrDefault("En proceso")
                               + porEstado.GetValueOrDefault("Planificada")
                               + porEstado.GetValueOrDefault("En calidad"),
            PromedioDiasCiclo = ciclosCompletos.Count == 0 ? 0 : Math.Round(ciclosCompletos.Average(), 2)
        };
    }

    public async Task<InformeCalidadDto> ObtenerInformeCalidadAsync()
    {
        var (total, aprobadas, defectuosas) = await _inspecciones.ObtenerResumenAsync();
        var porcentaje = total == 0 ? 0 : Math.Round((decimal)aprobadas / total * 100, 2);

        return new InformeCalidadDto
        {
            TotalInspecciones = total,
            Aprobadas = aprobadas,
            Defectuosas = defectuosas,
            PorcentajeAprobacion = porcentaje,
            CostoCalidadEstimado = defectuosas * CostoUnitarioDefecto
        };
    }

    public async Task<InformeInventarioDto> ObtenerInformeInventarioAsync()
    {
        var materias = (await _inventario.ObtenerConProveedorAsync()).ToList();
        var alertas = (await _inventario.ObtenerConAlertaReabastecimientoAsync()).ToList();

        return new InformeInventarioDto
        {
            TotalMateriasPrimas = materias.Count,
            ItemsConAlerta = alertas.Count,
            MaterialesPorReabastecer = alertas.Select(m => m.Nombre)
        };
    }

    public async Task<InformeProveedoresDto> ObtenerInformeProveedoresAsync()
    {
        var proveedores = (await _proveedores.ObtenerTodosAsync()).ToList();
        var mejores = await _proveedores.ObtenerConEvaluacionAsync();

        return new InformeProveedoresDto
        {
            TotalProveedores = proveedores.Count,
            PromedioEvaluacion = await _proveedores.ObtenerPromedioEvaluacionAsync(),
            MejoresProveedores = mejores.Take(3).Select(p => $"{p.Nombre} ({p.EvaluacionDesempenio})")
        };
    }
}
