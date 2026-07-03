using Microsoft.EntityFrameworkCore;
using SisTalleresApi.Models;

namespace SisTalleresApi.Data
{
    public class SisTalleresContext : DbContext
    {
        public SisTalleresContext(DbContextOptions<SisTalleresContext> options)
            : base(options)
        {
        }

        public DbSet<Taller> Talleres { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---------- TALLERES ----------
            modelBuilder.Entity<Taller>(entity =>
            {
                entity.ToTable("Talleres");
                entity.Property(t => t.TallerId).HasColumnName("taller_id");
                entity.Property(t => t.Nombre).HasColumnName("nombre");
                entity.Property(t => t.Descripcion).HasColumnName("descripcion");
                entity.Property(t => t.Fecha).HasColumnName("fecha");
                entity.Property(t => t.Ubicacion).HasColumnName("ubicacion");
            });

            // ---------- PARTICIPANTES ----------
            modelBuilder.Entity<Participante>(entity =>
            {
                entity.ToTable("Participantes");
                entity.Property(p => p.ParticipanteId).HasColumnName("participante_id");
                entity.Property(p => p.Nombre).HasColumnName("nombre");
                entity.Property(p => p.Apellido).HasColumnName("apellido");
                entity.Property(p => p.Email).HasColumnName("email");
                entity.Property(p => p.Telefono).HasColumnName("telefono");

                entity.HasIndex(p => p.Email).IsUnique();
            });

            // ---------- INSCRIPCIONES ----------
            modelBuilder.Entity<Inscripcion>(entity =>
            {
                entity.ToTable("Inscripciones");
                entity.Property(i => i.InscripcionId).HasColumnName("inscripcion_id");
                entity.Property(i => i.TallerId).HasColumnName("taller_id");
                entity.Property(i => i.ParticipanteId).HasColumnName("participante_id");
                entity.Property(i => i.FechaInscripcion).HasColumnName("fecha_inscripcion");

                entity.Property(i => i.Estado)
                      .HasColumnName("estado")
                      .HasConversion<string>();

                entity.HasIndex(i => new { i.TallerId, i.ParticipanteId }).IsUnique();

                entity.HasOne(i => i.Taller)
                      .WithMany(t => t.Inscripciones)
                      .HasForeignKey(i => i.TallerId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(i => i.Participante)
                      .WithMany(p => p.Inscripciones)
                      .HasForeignKey(i => i.ParticipanteId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}