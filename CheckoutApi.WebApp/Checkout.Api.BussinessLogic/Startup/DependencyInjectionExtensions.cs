using Checkout.Api.BussinessLogic.EndpointHandlers.Concrete;
using Checkout.Api.BussinessLogic.EndpointHandlers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Api.BussinessLogic.Startup
{
    public static class DependencyInjectionExtensions
    {
        public static void AddEndpointHandlers(this IServiceCollection services)
        {
            services.AddScoped<IAddItemHandler, AddItemHandler>();
            services.AddScoped<IBasketHandler, BasketHandler>();
        }
    }
}
