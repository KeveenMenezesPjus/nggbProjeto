using Microsoft.EntityFrameworkCore;

using nggbProjeto.Models;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Password).IsRequired();
        });
    }
}
