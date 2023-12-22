using Microsoft.EntityFrameworkCore;
using RouletteDemo.Api.Models;

namespace RouletteDemo.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Bet> Bets { set; get; }
        public DbSet<Spin> Spins { set; get; }
    }
}