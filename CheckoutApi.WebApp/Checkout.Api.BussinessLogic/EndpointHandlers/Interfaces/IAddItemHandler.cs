using Checkout.Api.BussinessLogic.Dtos.Request;
using CheckoutApi.Shared.Utils;

namespace Checkout.Api.BussinessLogic.EndpointHandlers.Interfaces
{
    public interface IAddItemHandler
    {
        Task<Result> AddNewItemInBasket(NewItemDto itemDto, int basketId);
    }
}
