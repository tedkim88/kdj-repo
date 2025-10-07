using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using POSystem.BLL;
using POSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSystem
{
    public static class POExtension
    {
        // This is an extension method that extends the IServiceCollection interface.
        // It is typically used in ASP.NET Core applications to configure and register services.

        // The method name can be anything, but it must match the name used when calling it in
        // your Program.cs file using builder.Services.XXXX(options => ...).
        public static void PODependencies(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {

            services.AddDbContext<POContext>(options);

            services.AddTransient<VendorService>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<POContext>();

                //  Create a new instance of WorkingVersionsService,
                //    passing the HogWoldContext instance aas a parameter

                return new VendorService(context);
            });

            services.AddTransient<PurchaseOrderService>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<POContext>();

                //  Create a new instance of WorkingVersionsService,
                //    passing the HogWoldContext instance aas a parameter

                return new PurchaseOrderService(context);
            });

            services.AddTransient<StockItemService>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<POContext>();

                //  Create a new instance of WorkingVersionsService,
                //    passing the HogWoldContext instance aas a parameter

                return new StockItemService(context);
            });
        }
    }
}
