using Domain;
using Microsoft.EntityFrameworkCore;
using MyNetwork.InfraData.Mapping;

namespace InfraData
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }

        // ~ Migrations ~
        // In Console:
        // cd ./InfraData/
        // dotnet ef migrations add Initial -c ApplicationDbContext -s ..\WebApi\ -v
        // dotnet ef database update -c ApplicationDbContext -s ..\WebApi\ -v
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                const string connection = "Data Source=database.db";

                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlite(connection)
                    .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Net");

            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}
