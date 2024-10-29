using Microsoft.EntityFrameworkCore;

namespace bailable_api.Models;

public class ContextDb : DbContext
{
    public ContextDb(DbContextOptions<ContextDb> options) : base(options)
    { }

    public DbSet<Evento> Eventos { get; set; }
}
