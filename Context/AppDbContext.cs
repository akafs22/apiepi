using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using apiepi.Models;
using apiepi.Controllers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace apiepi.Context;

public partial class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Colaborador> Colaboradors { get; set; }

    public virtual DbSet<Entrega> Entregas { get; set; }

    public virtual DbSet<Epi> Epis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=epis;User Id=postgres;Password=senai901;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Colaborador>(entity =>
        {
            entity.HasKey(e => e.CodigoId).HasName("colaborador_pkey");

            entity.ToTable("colaborador");

            entity.HasIndex(e => e.Cpf, "ck_cpf").IsUnique();

            entity.HasIndex(e => e.CodigoId, "idx_codigo_id");

            entity.Property(e => e.CodigoId)
                .ValueGeneratedNever()
                .HasColumnName("codigo_id");
            entity.Property(e => e.Cpf).HasColumnName("cpf");
            entity.Property(e => e.Ctps)
                .HasMaxLength(18)
                .HasColumnName("ctps");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(120)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(14)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.HasKey(e => e.EntregaId).HasName("entrega_pkey");

            entity.ToTable("entrega");

            entity.HasIndex(e => new { e.ColaboradorId, e.EpiId }, "idx_colab_epi");

            entity.HasIndex(e => e.EntregaId, "idx_entrega_id");

            entity.Property(e => e.EntregaId).HasColumnName("entrega_id");
            entity.Property(e => e.ColaboradorId).HasColumnName("colaborador_id");
            entity.Property(e => e.DtEntrega).HasColumnName("dt_entrega");
            entity.Property(e => e.DtValidade).HasColumnName("dt_validade");
            entity.Property(e => e.EpiId).HasColumnName("epi_id");

            entity.HasOne(d => d.Colaborador).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.ColaboradorId)
                .HasConstraintName("colaborador");

            entity.HasOne(d => d.Epi).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.EpiId)
                .HasConstraintName("epi");
        });

        modelBuilder.Entity<Epi>(entity =>
        {
            entity.HasKey(e => e.EpiId).HasName("epi_pkey");

            entity.ToTable("epi");

            entity.HasIndex(e => e.EpiId, "idx_epi_id");

            entity.Property(e => e.EpiId)
                .ValueGeneratedNever()
                .HasColumnName("epi_id");
            entity.Property(e => e.Nome)
                .HasMaxLength(300)
                .HasColumnName("nome");
            entity.Property(e => e.Observacao)
                .HasMaxLength(1000)
                .HasColumnName("observacao");
        });



        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
