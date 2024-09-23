using Identity.Application.Types.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Identity.Database;

public sealed class AppDbContext : DbContext
{
    #region DbSets

    public DbSet<User> Users { get; set; }

    #endregion

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.HasSequence<int>("UserId_HiLo").StartsAt(5).IncrementsBy(4);

        // Creating Model
        base.OnModelCreating(modelBuilder);
    }
}