using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CasoPropuesto_5.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InspeccionesCalidad> InspeccionesCalidads { get; set; }

    public virtual DbSet<MateriasPrima> MateriasPrimas { get; set; }

    public virtual DbSet<OrdenesProduccion> OrdenesProduccions { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=arcugave");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InspeccionesCalidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("inspecciones_calidad_pkey");

            entity.Property(e => e.FechaInspeccion).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.OrdenProduccion).WithMany(p => p.InspeccionesCalidads).HasConstraintName("inspecciones_calidad_orden_produccion_id_fkey");
        });

        modelBuilder.Entity<MateriasPrima>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("materias_primas_pkey");

            entity.Property(e => e.StockMinimo).HasDefaultValue(10);

            entity.HasOne(d => d.Proveedor).WithMany(p => p.MateriasPrimas)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("materias_primas_proveedor_id_fkey");
        });

        modelBuilder.Entity<OrdenesProduccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ordenes_produccion_pkey");

            entity.Property(e => e.Estado).HasDefaultValueSql("'En proceso'::character varying");
            entity.Property(e => e.FechaInicio).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("proveedores_pkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
