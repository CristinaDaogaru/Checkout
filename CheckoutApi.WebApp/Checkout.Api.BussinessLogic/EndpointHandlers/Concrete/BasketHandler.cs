using Checkout.Api.BussinessLogic.Dtos.Request;
using Checkout.Api.BussinessLogic.Dtos.Response;
using Checkout.Api.BussinessLogic.EndpointHandlers.Interfaces;
using Checkout.Api.BussinessLogic.Helpers;
using Checkout.Api.BussinessLogic.Utils;
using CheckoutApi.DataAccess.UnitOfWorkRelated.Interfaces;
using CheckoutApi.DataModels;
using System.Linq.Expressions;

namespace Checkout.Api.BussinessLogic.EndpointHandlers.Concrete
{
    public class BasketHandler : IBasketHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public BasketHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> AddNewBasket(NewBasketDto customerDto)
        {
            var newCustomer = new Customer
            {
                FullName = customerDto.CustomerFullName,
                PaysVAT = customerDto.PaysVAT,
            };
            var newBasket = new Basket
            {
                Customer = newCustomer,
                Close = false,
                IsPaid = false,
            };

            _unitOfWork.CustomerRepository.Add(newCustomer);
            _unitOfWork.BasketRepository.Add(newBasket);

            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Result<BasketDto>> GetBasket(int id)
        {
            var basket = await _unitOfWork.BasketRepository.GetByAsync(
                whereFilters: new Expression<Func<Basket, bool>>[] {
                  b => b.ID == id
                },
                includes: new Expression<Func<Basket, object>>[] {
                    b => b.Customer,
                    b=> b.ItemsInBaskets
                },
                asNoTracking: true);

            if (basket == null)
            {
                return GetBasketNotFoundResult(id);
            }

            var basketDto = CreateBasketDto(basket);
            return Result.Ok(basketDto);
        }
        public async Task<Result> Checkout(CheckoutDto checkout, int id)
        {
            var basket = await _unitOfWork.BasketRepository.GetByAsync(
                whereFilters: new Expression<Func<Basket, bool>>[] {
                  b => b.ID == id
                },
                asNoTracking: false);

            if (basket == null)
            {
                return GetBasketNotFoundResult(id);
            }

            basket.Close = checkout.Close;
            basket.IsPaid = checkout.Payed;

            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }

        private BasketDto CreateBasketDto(Basket basket)
        {
            var totalNet = GetTotalNet(basket.ItemsInBaskets);
            return new BasketDto
            {
                Id = basket.ID,
                Customer = basket.Customer.FullName,
                PaysVAT = basket.Customer.PaysVAT,
                Items = GetItems(basket.ItemsInBaskets),
                TotalGross = TotalGrossHelper.CalculateTotalGross(totalNet),
                TotalNet = totalNet
            };
        }

        private IEnumerable<ItemDto> GetItems(IEnumerable<ItemsInBasket> items)
        {
            foreach (var item in items)
            {
                yield return new ItemDto
                {
                    Name = item.Item.Name,
                    Price = item.Item.Price,
                };
            }
        }

        private decimal GetTotalNet(IEnumerable<ItemsInBasket> items)
        {
            var totalNet = (decimal)0;
            foreach (var item in items)
            {
                totalNet += item.Item.Price;
            }

            return totalNet;
        }

        private Result<BasketDto> GetBasketNotFoundResult(int id)
        {
            return Result.Fail<BasketDto>($"Basket with id {id} notFound");
        }


    }
}
