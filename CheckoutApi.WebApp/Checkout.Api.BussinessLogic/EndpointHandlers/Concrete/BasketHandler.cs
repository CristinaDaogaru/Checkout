using Checkout.Api.BussinessLogic.Dtos.Request;
using Checkout.Api.BussinessLogic.Dtos.Response;
using Checkout.Api.BussinessLogic.EndpointHandlers.Interfaces;
using Checkout.Api.BussinessLogic.Helpers;
using CheckoutApi.DataAccess.UnitOfWorkRelated.Interfaces;
using CheckoutApi.DataModels;
using CheckoutApi.Shared.Settings;
using CheckoutApi.Shared.Utils;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Checkout.Api.BussinessLogic.EndpointHandlers.Concrete
{
    public class BasketHandler : IBasketHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CheckoutApiSettings _checkoutApiSettings;
        public BasketHandler(IUnitOfWork unitOfWork, IOptions<CheckoutApiSettings> checkoutApiSettings)
        {
            _unitOfWork = unitOfWork;
            _checkoutApiSettings = checkoutApiSettings.Value;
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
            var items = await GetItemsForBasket(basket.ItemsInBaskets);
            var basketDto = CreateBasketDto(basket, items);

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

        private BasketDto CreateBasketDto(Basket basket, IEnumerable<Item> items)
        {
            var totalNet = GetTotalNet(items);
            return new BasketDto
            {
                Id = basket.ID,
                Customer = basket.Customer.FullName,
                PaysVAT = basket.Customer.PaysVAT,
                Items = GetItems(items),
                TotalGross = TotalGrossHelper.CalculateTotalGross(totalNet, _checkoutApiSettings.VAT),
                TotalNet = totalNet
            };
        }

        private IEnumerable<ItemDto> GetItems(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                yield return new ItemDto
                {
                    Name = item.Name,
                    Price = item.Price,
                };
            }
        }

        private decimal GetTotalNet(IEnumerable<Item> items)
        {
            var totalNet = (decimal)0;
            foreach (var item in items)
            {
                totalNet += item.Price;
            }

            return totalNet;
        }

        private Result<BasketDto> GetBasketNotFoundResult(int id)
        {
            return Result.Fail<BasketDto>($"Basket with id {id} notFound");
        }

        private async Task<IEnumerable<Item>> GetItemsForBasket(IEnumerable<ItemsInBasket> itemsInBaskets)
        {
            var itemsIds = itemsInBaskets.Select(item => item.ItemId);

            return await _unitOfWork.ItemRepository.GetAllByAsync(
                whereFilters: new Expression<Func<Item, bool>>[] {
                  item => itemsIds.Contains(item.ID)
                },
                asNoTracking: true);
        }

    }
}
