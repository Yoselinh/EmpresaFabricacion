namespace CasoPropuesto_5.Dtos;

public class InformeGeneralDto
{
    public InformeProduccionDto Produccion { get; set; } = new();
    public InformeCalidadDto Calidad { get; set; } = new();
    public InformeInventarioDto Inventario { get; set; } = new();
    public InformeProveedoresDto Proveedores { get; set; } = new();
}

public class InformeProduccionDto
{
    public int TotalOrdenes { get; set; }
    public Dictionary<string, int> OrdenesPorEstado { get; set; } = new();
    public int OrdenesEntregadas { get; set; }
    public int OrdenesEnProceso { get; set; }
    public double PromedioDiasCiclo { get; set; }
}

public class InformeCalidadDto
{
    public int TotalInspecciones { get; set; }
    public int Aprobadas { get; set; }
    public int Defectuosas { get; set; }
    public decimal PorcentajeAprobacion { get; set; }
    public decimal CostoCalidadEstimado { get; set; }
}

public class InformeInventarioDto
{
    public int TotalMateriasPrimas { get; set; }
    public int ItemsConAlerta { get; set; }
    public IEnumerable<string> MaterialesPorReabastecer { get; set; } = [];
}

public class InformeProveedoresDto
{
    public int TotalProveedores { get; set; }
    public decimal? PromedioEvaluacion { get; set; }
    public IEnumerable<string> MejoresProveedores { get; set; } = [];
}
