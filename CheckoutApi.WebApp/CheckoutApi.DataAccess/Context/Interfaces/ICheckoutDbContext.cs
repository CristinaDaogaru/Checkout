using CheckoutApi.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CheckoutApi.DataAccess.Contect.Interfaces
{
    public interface ICheckoutDbContext : IDisposable
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Basket> Baskets { get; set; }
        DbSet<ItemsInBasket> ItemsInBaskets { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
