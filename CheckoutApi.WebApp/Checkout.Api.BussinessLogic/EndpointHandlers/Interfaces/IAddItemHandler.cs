using Checkout.Api.BussinessLogic.Dtos.Request;
using Checkout.Api.BussinessLogic.Utils;

namespace Checkout.Api.BussinessLogic.EndpointHandlers.Interfaces
{
    public interface IAddItemHandler
    {
        Task<Result> AddNewItemInBasket(NewItemDto itemDto, int basketId);
    }
}
