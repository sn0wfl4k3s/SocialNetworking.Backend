using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using InfraData.Mapping;

namespace InfraData
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<File> Files { get; set; }

        // ~ Migrations ~
        // In Console:
        // cd ./InfraData/
        // dotnet ef migrations add Initial -c ApplicationDbContext -s ..\WebApi\ -v
        // dotnet ef database update -c ApplicationDbContext -s ..\WebApi\ -v
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlite("Data Source=database.db")
                    .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Net");

            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new FileMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
        }
    }
}
