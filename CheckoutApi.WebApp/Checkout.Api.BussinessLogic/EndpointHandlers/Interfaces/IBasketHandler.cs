using Checkout.Api.BussinessLogic.Dtos.Request;
using Checkout.Api.BussinessLogic.Dtos.Response;
using CheckoutApi.Shared.Utils;

namespace Checkout.Api.BussinessLogic.EndpointHandlers.Interfaces
{
    public interface IBasketHandler
    {
        Task<Result> AddNewBasket(NewBasketDto customerDto);

        Task<Result<BasketDto>> GetBasket(int id);

        Task<Result> Checkout(CheckoutDto checkout, int id);
    }
}
