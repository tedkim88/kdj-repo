using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSystem.ViewModels
{
    public class RentalSearchView
    {
        public int RentalID { get; set; }
        public string City { get; set; }
        public string Community { get; set; }
        public string RentalType { get; set; }
        public int MaxVacancy  { get; set; }
        public int AvailableVacancy { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal? DamageDeposit { get; set; }
    }
}
