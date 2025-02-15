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
                //Retrieve an instance of HogWildContext from the service provder.
                var context = ServiceProvider.GetService<SalesContext>();

                //create a new instance of workingversionsservice,
                //passing the hogwildcontext instance as a parameter

                return new CategoryService(context);


            });

            services.AddTransient<EmployeeService>((ServiceProvider) =>
            {
                //Retrieve an instance of HogWildContext from the service provder.
                var context = ServiceProvider.GetService<SalesContext>();

                //create a new instance of workingversionsservice,
                //passing the hogwildcontext instance as a parameter

                return new EmployeeService(context);
            });

            services.AddTransient<CouponService>((ServiceProvider) =>
            {
                //Retrieve an instance of HogWildContext from the service provder.
                var context = ServiceProvider.GetService<SalesContext>();

                //create a new instance of workingversionsservice,
                //passing the hogwildcontext instance as a parameter

                return new CouponService(context);
            });

            services.AddTransient<SaleService>((ServiceProvider) =>
            {
                //Retrieve an instance of HogWildContext from the service provder.
                var context = ServiceProvider.GetService<SalesContext>();
                var refundService = ServiceProvider.GetService<RefundService>();
                //create a new instance of workingversionsservice,
                //passing the hogwildcontext instance as a parameter

                return new SaleService(context, refundService);
            });

            services.AddTransient<RefundService>((ServiceProvider) =>
            {
                //Retrieve an instance of HogWildContext from the service provder.
                var context = ServiceProvider.GetService<SalesContext>();

                //create a new instance of workingversionsservice,
                //passing the hogwildcontext instance as a parameter

                return new RefundService(context);
            });




            //addscoped을쓸지 / addsingleton을쓸지 둘중에 하나결정해야하는듯??
            //builder.Services.AddScoped<AppState>(); //added
            //builder.Services.AddSingleton<AppState>(); //added(to prolong the lifecycle of Appstate variable??

            //program.cs에 등록했으면 다시 등록할 필요가 없는듯..?중복등록피해야하나...?확인
            services.AddSingleton<AppState>();


        }



    }
}
