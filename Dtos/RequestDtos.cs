namespace CasoPropuesto_5.Dtos;

public class OrdenProduccionDto
{
    public string CodigoOrden { get; set; } = null!;
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public string Estado { get; set; } = "Planificada";
}

public class CambiarEstadoDto
{
    public string Estado { get; set; } = null!;
}

public class InspeccionCalidadDto
{
    public int OrdenProduccionId { get; set; }
    public string Etapa { get; set; } = null!;
    public string Resultado { get; set; } = "Aprobado";
    public string? Observaciones { get; set; }
}

public class MarcarDefectuosoDto
{
    public string? Observaciones { get; set; }
}

public class MateriaPrimaDto
{
    public string Nombre { get; set; } = null!;
    public int StockActual { get; set; }
    public int StockMinimo { get; set; } = 10;
    public int? ProveedorId { get; set; }
}

public class ReabastecerDto
{
    public int Cantidad { get; set; }
}

public class ProveedorDto
{
    public string Nombre { get; set; } = null!;
    public string? Contacto { get; set; }
    public decimal? EvaluacionDesempenio { get; set; }
}

public class EvaluacionProveedorDto
{
    public decimal EvaluacionDesempenio { get; set; }
}
