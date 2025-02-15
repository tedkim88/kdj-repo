using RMSSystem.DAL;
using RMSSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSystem.BLL
{
    public class RenterService
    {

        private readonly RMSContext _rentalContext;


        internal RenterService(RMSContext rentalContext)
        {
            //Initialize the _hogWildContext field with the provided HogWildContext instance
            _rentalContext = rentalContext;
        }


        public RenterView getRenter(int studentNumber, int rentalID)
        {

            var getEmergencyInfo = _rentalContext.Renters.Where(x => x.StudentNumber == studentNumber).FirstOrDefault();

            var matchingRental = _rentalContext.Rentals.Where(x => x.RentalID == rentalID &&
                                                                   x.Renters.Any(a => a.StudentNumber == studentNumber))
                                                             .FirstOrDefault();
            int matchingRenterID = 0;
            if (matchingRental != null)
            {
                matchingRenterID = matchingRental.Renters.Where(x => x.StudentNumber == studentNumber)
                                       .Select(x => x.RenterID).FirstOrDefault();
            }





            return _rentalContext.Students.Where(x => x.StudentNumber == studentNumber &&
                                                       x.RemoveFromViewFlag == false)
                                           .Select(x => new RenterView
                                           {
                                               RenterID = matchingRenterID,
                                               StudentNumber = x.StudentNumber,
                                               StudentName = x.FirstName + " " + x.LastName,
                                               EmergencyContactName = getEmergencyInfo == null ? "" : getEmergencyInfo.EmergencyContactName,
                                               EmergencyContactNumber = getEmergencyInfo == null ? "" : getEmergencyInfo.EmergencyContactNumber
                                           })
                                           .FirstOrDefault();
        }



    }
}
