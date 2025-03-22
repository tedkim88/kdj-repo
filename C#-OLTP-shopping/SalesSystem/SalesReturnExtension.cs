using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesSystem.BLL;
using SalesSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem
{
    public static class SalesReturnExtension
    {
        public static void SalesDependencies(this IServiceCollection services,
                 Action<DbContextOptionsBuilder> options)
        {



            services.AddDbContext<SalesContext>(options);


            services.AddTransient<CategoryService>((ServiceProvider) =>
            {
                
               var context = ServiceProvider.GetService<SalesContext>();
             
                return new CategoryService(context);


            });

            services.AddTransient<EmployeeService>((ServiceProvider) =>
            {

                var context = ServiceProvider.GetService<SalesContext>();

                return new EmployeeService(context);
            });

            services.AddTransient<CouponService>((ServiceProvider) =>
            {
             
                var context = ServiceProvider.GetService<SalesContext>();

           
                return new CouponService(context);
            });

            services.AddTransient<SaleService>((ServiceProvider) =>
            {
                
                var context = ServiceProvider.GetService<SalesContext>();
                var refundService = ServiceProvider.GetService<RefundService>();
             

                return new SaleService(context, refundService);
            });

            services.AddTransient<RefundService>((ServiceProvider) =>
            {
              
                var context = ServiceProvider.GetService<SalesContext>();

          
                return new RefundService(context);
            });


          
            services.AddSingleton<AppState>();


        }



    }
}
