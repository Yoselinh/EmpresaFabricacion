using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CasoPropuesto_5.Models;

[Table("proveedores")]
public partial class Proveedore
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("contacto")]
    [StringLength(100)]
    public string? Contacto { get; set; }

    [Column("evaluacion_desempenio")]
    [Precision(3, 2)]
    public decimal? EvaluacionDesempenio { get; set; }

    [InverseProperty("Proveedor")]
    public virtual ICollection<MateriasPrima> MateriasPrimas { get; set; } = new List<MateriasPrima>();
}
