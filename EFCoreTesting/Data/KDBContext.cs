using EFCoreTesting.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTesting.Data
{
    public class KDBContext : DbContext
    {
        public KDBContext(DbContextOptions<KDBContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        
    }
}
