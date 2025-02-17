using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RMSSystem.BLL;
using RMSSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSystem
{
    public static class RentalExtension
    {


        //added db
        public static void AddBackendDependencies(this IServiceCollection services,
Action<DbContextOptionsBuilder> options)
        {

            services.AddDbContext<RMSContext>(options);



            services.AddTransient<RentalService>((ServiceProvider) =>
            {
                //Retrieve an instance of HogWildContext from the service provder.
                var context = ServiceProvider.GetService<RMSContext>();

                //create a new instance of workingversionsservice,
                //passing the hogwildcontext instance as a parameter

                return new RentalService(context);

            });

            services.AddTransient<StudentService>((ServiceProvider) =>
            {
                //Retrieve an instance of HogWildContext from the service provder.
                var context = ServiceProvider.GetService<RMSContext>();

                //create a new instance of workingversionsservice,
                //passing the hogwildcontext instance as a parameter

                return new StudentService(context);

            });


            services.AddTransient<RenterService>((ServiceProvider) =>
            {
                //Retrieve an instance of HogWildContext from the service provder.
                var context = ServiceProvider.GetService<RMSContext>();

                //create a new instance of workingversionsservice,
                //passing the hogwildcontext instance as a parameter

                return new RenterService(context);

            });

            services.AddTransient<AddressService>((ServiceProvider) =>
            {
                //Retrieve an instance of HogWildContext from the service provder.
                var context = ServiceProvider.GetService<RMSContext>();

                //create a new instance of workingversionsservice,
                //passing the hogwildcontext instance as a parameter

                return new AddressService(context);

            });



           



        }








    }
}
