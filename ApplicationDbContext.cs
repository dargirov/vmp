using Microsoft.EntityFrameworkCore;
using VeganMap.Models;

namespace VeganMap;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<City> Cities { get; set; }

    public DbSet<Place> Places { get; set; }

    public DbSet<PlacePicture> PlacePictures { get; set; }

    public DbSet<Feedback> Feedbacks { get; set; }

    public DbSet<Log> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<City>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Place>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<PlacePicture>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Feedback>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Log>().HasQueryFilter(x => !x.IsDeleted);
    }
}
