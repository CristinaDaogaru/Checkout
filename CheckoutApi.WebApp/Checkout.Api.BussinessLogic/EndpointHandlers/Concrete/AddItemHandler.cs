using Checkout.Api.BussinessLogic.Dtos.Request;
using Checkout.Api.BussinessLogic.EndpointHandlers.Interfaces;
using Checkout.Api.BussinessLogic.Utils;
using CheckoutApi.DataAccess.UnitOfWorkRelated.Interfaces;
using CheckoutApi.DataModels;
using System.Linq.Expressions;

namespace Checkout.Api.BussinessLogic.EndpointHandlers.Concrete
{
    public class AddItemHandler : IAddItemHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddItemHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> AddNewItemInBasket(NewItemDto itemDto, int basketId)
        {
            var basket = await _unitOfWork.BasketRepository.GetByAsync(
                whereFilters: new Expression<Func<Basket, bool>>[] {
                  basket=> basket.ID==basketId
                },
                asNoTracking: true);

            if (basket == null)
            {
                GetBasketNotFoundResult(basketId);
            }

            var item = new Item
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
            };

            var itemInBaskets = new ItemsInBasket
            {
                Basket = basket,
                Item = item,
            };

            item.ItemsInBaskets.Add(itemInBaskets);

            _unitOfWork.ItemRepository.Add(item);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }

        private Result GetBasketNotFoundResult(int id)
        {
            return Result.Fail($"Basket with id {id} notFound");
        }
    }
}
