using Microsoft.EntityFrameworkCore;
using GestionEstudiantes.Models;

namespace GestionEstudiantes.Data
{
    public class EstudianteDbContext : DbContext
    {
        public DbSet<Estudiante> Estudiantes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Usar SQLite para desarrollo local
            string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GestionEstudiantes", "estudiantes.db");
            Directory.CreateDirectory(Path.GetDirectoryName(databasePath));
            
            optionsBuilder.UseSqlite($"Data Source={databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Email)
                    .HasMaxLength(100);
                
                entity.Property(e => e.Telefono)
                    .HasMaxLength(20);
                
                entity.Property(e => e.Edad)
                    .IsRequired();
                
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("datetime('now')");
            });
        }
    }
}
