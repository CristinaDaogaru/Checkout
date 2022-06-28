using CheckoutApi.DataAccess.Contect.Interfaces;
using CheckoutApi.DataAccess.Repositories.Concrete.BaseRepository;
using CheckoutApi.DataAccess.Repositories.Interfaces;
using CheckoutApi.DataModels;

namespace CheckoutApi.DataAccess.Repositories.Concrete
{
    public class CustomerRepository : BaseRepository<Customer, int>, ICustomerRepository
    {
        public CustomerRepository(ICheckoutDbContext checkoutDbContext) : base(checkoutDbContext)
        {
        }
    }
}
