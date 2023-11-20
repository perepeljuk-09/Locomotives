using Locomotives.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Locomotives.Infrastructure.DataBases
{
    public class PgContext : DbContext
    {
        
        public DbSet<Depot> Depots { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Locomotive> Locomotives { get; set; }
        public DbSet<LocomotiveCategories> LocomotiveCategories { get; set; }
        public DbSet<LocomotiveCategoriesDrivers> LocomotiveCategoriesDrivers { get; set; }
        
        public PgContext()
        {
        }
        public PgContext(DbContextOptions<PgContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=locomotivesDb;Username=postgres;Password=root");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Locomotives.Domain"));
        }
    }
}
