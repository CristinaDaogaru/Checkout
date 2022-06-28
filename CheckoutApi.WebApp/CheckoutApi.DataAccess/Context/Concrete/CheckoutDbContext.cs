using CheckoutApi.DataAccess.Contect.Interfaces;
using CheckoutApi.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CheckoutApi.DataAccess.Contect.Concrete
{
    public class CheckoutDbContext : DbContext, ICheckoutDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<ItemsInBasket> ItemsInBaskets { get; set; }

        public CheckoutDbContext(string connectionString) : base(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
        {
        }
        public CheckoutDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemsInBasket>()
                .HasKey(itemsInBasket => new { itemsInBasket.BasketId, itemsInBasket.ItemId });

            modelBuilder.Entity<ItemsInBasket>()
                .HasOne(itemsInBasket => itemsInBasket.Item)
                .WithMany(item => item.ItemsInBaskets);

            modelBuilder.Entity<ItemsInBasket>()
                .HasOne(itemsInBasket => itemsInBasket.Basket)
                .WithMany(basket => basket.ItemsInBaskets);
        }
    }
}
