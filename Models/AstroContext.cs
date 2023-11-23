using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppWeb_Astro.Models;

public partial class AstroContext : DbContext
{
    public AstroContext()
    {
    }

    public AstroContext(DbContextOptions<AstroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Astronauta> Astronautas { get; set; }

    public virtual DbSet<MisionesEspaciale> MisionesEspaciales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MSI\\SQLEXPRESS01; Database=Astro; Trusted_Connection=SSPI; Encrypt=false; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Astronauta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Astronau__3214EC07DDA39D1B");

            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
            entity.Property(e => e.ImagenUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Redes)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.MisionEspacials).WithMany(p => p.Astronauta)
                .UsingEntity<Dictionary<string, object>>(
                    "AstronautasMisionesEspaciale",
                    r => r.HasOne<MisionesEspaciale>().WithMany()
                        .HasForeignKey("MisionEspacialId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Astronaut__Misio__5FB337D6"),
                    l => l.HasOne<Astronauta>().WithMany()
                        .HasForeignKey("AstronautaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Astronaut__Astro__5EBF139D"),
                    j =>
                    {
                        j.HasKey("AstronautaId", "MisionEspacialId").HasName("PK__Astronau__4D8CB903C93AD334");
                        j.ToTable("AstronautasMisionesEspaciales");
                    });
        });

        modelBuilder.Entity<MisionesEspaciale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Misiones__3214EC07DD1789F1");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
