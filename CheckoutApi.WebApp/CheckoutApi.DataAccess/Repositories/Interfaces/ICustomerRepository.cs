using CheckoutApi.DataAccess.Repositories.Interfaces.Base;
using CheckoutApi.DataModels;

namespace CheckoutApi.DataAccess.Repositories.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer, int>
    {
    }
}
