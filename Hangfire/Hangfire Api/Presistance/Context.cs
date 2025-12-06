using Hangfire_Api.Entities;

using Microsoft.EntityFrameworkCore;

namespace Hangfire_Api.Presistance;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.Entity<Category>()
             .Property(x => x.Id)
             .HasDefaultValueSql("NEWSEQUENTIALID()");

        _ = modelBuilder.Entity<Product>()
            .Property(x => x.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");
    }
}