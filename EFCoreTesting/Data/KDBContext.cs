using EFCoreTesting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreTesting.Data
{
    public class KDBContext : DbContext
    {
        public KDBContext(DbContextOptions<KDBContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
