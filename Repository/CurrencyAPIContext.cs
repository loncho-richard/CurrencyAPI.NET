using Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace Data
{
    public class CurrencyAPIContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Currency> Currencys { get; set; }
        
        public CurrencyAPIContext(DbContextOptions<CurrencyAPIContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
