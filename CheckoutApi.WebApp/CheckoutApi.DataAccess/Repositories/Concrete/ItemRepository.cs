using CheckoutApi.DataAccess.Contect.Interfaces;
using CheckoutApi.DataAccess.Repositories.Concrete.BaseRepository;
using CheckoutApi.DataAccess.Repositories.Interfaces;
using CheckoutApi.DataModels;

namespace CheckoutApi.DataAccess.Repositories.Concrete
{
    public class ItemRepository : BaseRepository<Item, int>, IItemRepository
    {
        public ItemRepository(ICheckoutDbContext checkoutDbContext) : base(checkoutDbContext)
        {
        }
    }
}
