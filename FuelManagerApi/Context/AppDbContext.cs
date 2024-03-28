using FuelManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FuelManagerApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Veiculo> Veiculos { get; set;}
        public DbSet<Consumo> Consumos { get; set;}
    }
}
