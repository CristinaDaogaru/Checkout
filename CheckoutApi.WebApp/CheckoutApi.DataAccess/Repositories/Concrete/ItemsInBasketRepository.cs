
using CheckoutApi.DataAccess.Contect.Interfaces;
using CheckoutApi.DataAccess.Repositories.Concrete.BaseRepository;
using CheckoutApi.DataAccess.Repositories.Interfaces;
using CheckoutApi.DataModels;

namespace CheckoutApi.DataAccess.Repositories.Concrete
{
    public class ItemsInBasketRepository : BaseRepository<ItemsInBasket, int>, IItemsInBasketRepository
    {
        public ItemsInBasketRepository(ICheckoutDbContext checkoutDbContext) : base(checkoutDbContext)
        {
        }
    }
}
