using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CasoPropuesto_5.Models;

[Table("inspecciones_calidad")]
public partial class InspeccionesCalidad
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("orden_produccion_id")]
    public int OrdenProduccionId { get; set; }

    [Column("etapa")]
    [StringLength(50)]
    public string Etapa { get; set; } = null!;

    [Column("resultado")]
    [StringLength(20)]
    public string Resultado { get; set; } = null!;

    [Column("observaciones")]
    public string? Observaciones { get; set; }

    [Column("fecha_inspeccion", TypeName = "timestamp without time zone")]
    public DateTime? FechaInspeccion { get; set; }

    [ForeignKey("OrdenProduccionId")]
    [InverseProperty("InspeccionesCalidads")]
    public virtual OrdenesProduccion OrdenProduccion { get; set; } = null!;
}
