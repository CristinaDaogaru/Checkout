using CheckoutApi.DataAccess.Contect.Concrete;
using CheckoutApi.DataAccess.Contect.Interfaces;
using CheckoutApi.DataAccess.UnitOfWorkRelated.Concrete;
using CheckoutApi.DataAccess.UnitOfWorkRelated.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CheckoutApi.DataAccess.Startup
{
    public static class DependencyInjectionExtensionsDataAccess
    {
        public static void AddDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ICheckoutDbContext, CheckoutDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

    }
}
