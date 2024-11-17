using Microsoft.EntityFrameworkCore;

namespace bailable_api.Models;

public class ContextDb : DbContext
{
    public ContextDb(DbContextOptions<ContextDb> options) : base(options)
    { }

    public DbSet<Local> Locales { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<User> Users { get; set; }

}
