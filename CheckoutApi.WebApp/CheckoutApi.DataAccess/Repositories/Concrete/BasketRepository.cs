using CheckoutApi.DataAccess.Contect.Interfaces;
using CheckoutApi.DataAccess.Repositories.Concrete.BaseRepository;
using CheckoutApi.DataAccess.Repositories.Interfaces;
using CheckoutApi.DataModels;

namespace CheckoutApi.DataAccess.Repositories.Concrete
{
    public class BasketRepository : BaseRepository<Basket, int>, IBasketRepository
    {
        public BasketRepository(ICheckoutDbContext checkoutDbContext) : base(checkoutDbContext)
        {
        }
    }
}
