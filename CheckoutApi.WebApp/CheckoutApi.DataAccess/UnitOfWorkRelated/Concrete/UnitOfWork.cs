using CheckoutApi.DataAccess.Contect.Interfaces;
using CheckoutApi.DataAccess.Repositories.Concrete;
using CheckoutApi.DataAccess.Repositories.Interfaces;
using CheckoutApi.DataAccess.UnitOfWorkRelated.Interfaces;

namespace CheckoutApi.DataAccess.UnitOfWorkRelated.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICheckoutDbContext _dbContext;
        private bool _disposedValue;

        private IBasketRepository _basketRepository;
        public IBasketRepository BasketRepository => _basketRepository ??= new BasketRepository(_dbContext);

        private IItemRepository _itemRepository;
        public IItemRepository ItemRepository => _itemRepository ??= new ItemRepository(_dbContext);

        private ICustomerRepository _customerRepository;
        public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(_dbContext);

        private IItemsInBasketRepository _itemsInBasketRepository;
        public IItemsInBasketRepository ItemsInBasketRepository => _itemsInBasketRepository ??= new ItemsInBasketRepository(_dbContext);


        public UnitOfWork(ICheckoutDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
