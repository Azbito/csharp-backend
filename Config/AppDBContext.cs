using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Config
{
    public class AppDbContext : DbContext
    {
        private readonly IUtils _utils;

        public AppDbContext(DbContextOptions<AppDbContext> options, IUtils utils)
            : base(options)
        {
            _utils = utils;
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Post> Posts => Set<Post>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");

            modelBuilder
                .Entity<User>()
                .Property(u => u.Id)
                .HasDefaultValueSql("CONCAT('U-', SUBSTRING(CONVERT(VARCHAR(36), NEWID()), 1, 8))");

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.Name == "UserName")
                    {
                        property.SetColumnName("username");
                    }
                    else
                    {
                        property.SetColumnName(_utils.StringCase.ToSnakeCase(property.Name));
                    }
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
