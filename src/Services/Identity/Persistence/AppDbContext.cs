using Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence
{
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

            // Creating Model
            base.OnModelCreating(modelBuilder);
        }
    }
}