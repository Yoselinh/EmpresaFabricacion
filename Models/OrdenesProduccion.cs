using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CasoPropuesto_5.Models;

[Table("ordenes_produccion")]
[Index("CodigoOrden", Name = "ordenes_produccion_codigo_orden_key", IsUnique = true)]
public partial class OrdenesProduccion
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("codigo_orden")]
    [StringLength(50)]
    public string CodigoOrden { get; set; } = null!;

    [Column("fecha_inicio", TypeName = "timestamp without time zone")]
    public DateTime? FechaInicio { get; set; }

    [Column("fecha_fin", TypeName = "timestamp without time zone")]
    public DateTime? FechaFin { get; set; }

    [Column("estado")]
    [StringLength(30)]
    public string Estado { get; set; } = null!;

    [InverseProperty("OrdenProduccion")]
    public virtual ICollection<InspeccionesCalidad> InspeccionesCalidads { get; set; } = new List<InspeccionesCalidad>();
}
