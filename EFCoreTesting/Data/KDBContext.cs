using EFCoreTesting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreTesting.Data
{
    public class KDBContext : DbContext
    {
        public KDBContext(DbContextOptions<KDBContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }

        public DbSet<APLink> APLink { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ProviderInfo>().HasNoKey();
            //modelBuilder.Entity<CompanyInfo>().HasNoKey();
            //modelBuilder.Entity<DateInfo>().HasNoKey();
        }

    }
}
