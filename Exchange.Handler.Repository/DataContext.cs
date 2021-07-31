using Microsoft.EntityFrameworkCore;
using Exchange.Handler.Repository.Entities;

namespace Exchange.Handler.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<TradeType> TradeTypes { get; set; }
        public DbSet<GL> GL { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GL>()
            .HasKey(x => x.Id);

            modelBuilder.Entity<GL>()
            .Property(x => x.TxType)
            .HasConversion<int>();

            modelBuilder.Entity<GL>()
                .HasOne(x => x.Trade)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.TradeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Person>()
            .HasKey(x => x.Id);

            modelBuilder.Entity<Trade>()
            .HasKey(x => x.Id);

            modelBuilder.Entity<Trade>()
            .Property(x => x.Title).HasMaxLength(256);

            modelBuilder.Entity<Trade>()
            .Property(x => x.Description).HasMaxLength(2000);

            modelBuilder.Entity<Trade>()
               .HasOne(x => x.TradeType)
               .WithMany(x => x.Trades)
               .HasForeignKey(x => x.TradeTypeId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TradeType>()
            .HasKey(x => x.Id);

        }
    }
}
