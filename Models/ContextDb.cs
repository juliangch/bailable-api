using Microsoft.EntityFrameworkCore;

namespace bailable_api.Models;

public class ContextDb : DbContext
{
    public ContextDb(DbContextOptions<ContextDb> options) : base(options)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Reserva>().HasOne<User>(s => s.Usuario).WithMany(u => u.Reservas).HasForeignKey(u => u.UsuarioId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reserva>()
              .HasOne(r => r.Evento)
              .WithMany()
              .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reserva>()
        .HasMany(r => r.Servicios)
        .WithMany(s => s.Reservas)
        .UsingEntity<Dictionary<string, object>>(
            "ReservaServicio",
            j => j
                .HasOne<Servicio>()
                .WithMany()
                .HasForeignKey("ServicioId")
                .OnDelete(DeleteBehavior.Restrict),
            j => j
                .HasOne<Reserva>()
                .WithMany()
                .HasForeignKey("ReservaId")
                .OnDelete(DeleteBehavior.Restrict)
        );
    }
    public DbSet<Local> Locales { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
}
