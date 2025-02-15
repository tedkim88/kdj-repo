using RMSSystem.DAL;
using RMSSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSystem.BLL
{
    public class AddressService
    {

        private readonly RMSContext _rentalContext;



        internal AddressService(RMSContext rentalContext)
        {
            //Initialize the _hogWildContext field with the provided HogWildContext instance
            _rentalContext = rentalContext;
        }



        public AddressView GetNextUnusedAddress()
        {
            return _rentalContext.Addresses
                .Where(x => !x.RemoveFromViewFlag && !x.Rentals.Any())
                .Select(x => new AddressView()
                {
                    AddressID = x.AddressID,
                    Unit = x.Unit,
                    Number = x.Number,
                    Street = x.Street,
                    Community = x.Community,
                    City = x.City,
                    ProvinceState = x.ProvinceState,
                    CountryCode = x.CountryCode,
                    PostalCodeZip = x.PostalCodeZip,
                    LandLordID = x.LandLordID,
                    LandLord = x.LandLord.Name
                }
                ).FirstOrDefault();
        }
    }
}
