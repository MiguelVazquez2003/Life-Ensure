using System;
using System.Collections.Generic;
using LifeEnsure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LifeEnsure.Data;

public partial class TigreHacksContext : DbContext
{
    public TigreHacksContext()
    {
    }

    public TigreHacksContext(DbContextOptions<TigreHacksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accidente> Accidentes { get; set; }

    public virtual DbSet<Carro> Carros { get; set; }

    public virtual DbSet<HeatmapDatum> HeatmapData { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accidente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__accident__3213E83FDACC592F");

            entity.ToTable("accidente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Causa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("causa");
            entity.Property(e => e.Colonia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("colonia");
            entity.Property(e => e.Cruce)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cruce");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.IdVehiculo).HasColumnName("id_vehiculo");
            entity.Property(e => e.Latitud)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("latitud");
            entity.Property(e => e.Lesionados).HasColumnName("lesionados");
            entity.Property(e => e.Longitud)
                .HasColumnType("decimal(10, 6)")
                .HasColumnName("longitud");
            entity.Property(e => e.Muertos).HasColumnName("muertos");
            entity.Property(e => e.Municipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("municipio");
            entity.Property(e => e.NombreVialidad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_vialidad");
            entity.Property(e => e.Sentido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sentido");
            entity.Property(e => e.SituacionClimatica)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("situacion_climatica");
            entity.Property(e => e.SituacionPavimento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("situacion_pavimento");
            entity.Property(e => e.TipoAccidente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_accidente");
            entity.Property(e => e.TipoVialidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_vialidad");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Accidentes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__accidente__id_us__3D5E1FD2");

            entity.HasOne(d => d.IdVehiculoNavigation).WithMany(p => p.Accidentes)
                .HasForeignKey(d => d.IdVehiculo)
                .HasConstraintName("FK__accidente__id_ve__3C69FB99");
        });

        modelBuilder.Entity<Carro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__carro__3213E83FC23B0391");

            entity.ToTable("carro");

            entity.HasIndex(e => e.Placas, "UQ__carro__A3547E398688D141").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Modelo).HasColumnName("modelo");
            entity.Property(e => e.Placas)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("placas");
            entity.Property(e => e.ValorDeducible).HasColumnName("valor_deducible");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Carros)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__carro__id_usuari__398D8EEE");
        });

        modelBuilder.Entity<HeatmapDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HeatmapD__3213E83FE6A78F49");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuario__3213E83FC6C994BC");

            entity.ToTable("usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Genero)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("genero");
            entity.Property(e => e.Licencia).HasColumnName("licencia");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
