using CaseStudies.Core.Geography;
using Microsoft.EntityFrameworkCore;

namespace WorldCities.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base()
        {

        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<Country>().ToTable("Country");
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }
    }
}
