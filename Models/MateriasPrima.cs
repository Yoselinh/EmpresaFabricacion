using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CasoPropuesto_5.Models;

[Table("materias_primas")]
public partial class MateriasPrima
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("stock_actual")]
    public int StockActual { get; set; }

    [Column("stock_minimo")]
    public int StockMinimo { get; set; }

    [Column("proveedor_id")]
    public int? ProveedorId { get; set; }

    [ForeignKey("ProveedorId")]
    [InverseProperty("MateriasPrimas")]
    public virtual Proveedore? Proveedor { get; set; }
}
