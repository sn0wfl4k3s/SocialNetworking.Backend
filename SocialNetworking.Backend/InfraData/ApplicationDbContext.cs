using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace InfraData
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }

        // ~ Migrations ~
        // In Console:
        // cd ./MyNetwork.InfraData/
        // dotnet ef migrations add Initial -c ApplicationDbContext -s ..\MyNetwork.WebApi\
        // dotnet ef database update -c ApplicationDbContext -s ..\MyNetwork.WebApi\
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var configure = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.{environment}.json")
                    .Build();

                var connection = configure.GetConnectionString("DefaultConnection");

                optionsBuilder
                    .UseLazyLoadingProxies()
                    //.UseSqlServer(connection)
                    .EnableSensitiveDataLogging();
            }
        }
    }
}
