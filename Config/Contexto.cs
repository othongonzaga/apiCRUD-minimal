using Microsoft.EntityFrameworkCore;
using WebMinimalApiOracle.Models;

namespace WebMinimalApiOracle.Config
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options):base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Produto> Produto { get; set; }
    }
}