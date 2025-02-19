using SalesSystem.DAL;
using SalesSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.BLL
{
    public class CouponService
    {

        #region Fields
       
        private readonly SalesContext _salesContext;

        #endregion

        internal CouponService(SalesContext salesContext)
        {
           
            _salesContext = salesContext;

        }

        public int GetCouponRate(string couponValue)
        {
            if(string.IsNullOrWhiteSpace(couponValue))
            {
                throw new ArgumentException("Please enter your coupon for validation.(case-sensitive)");
            }

            var matchingCoupon = _salesContext.Coupons
                                   .AsEnumerable() 
                                   .Where(x => x.CouponIDValue.Equals(couponValue, StringComparison.Ordinal))
                                   .FirstOrDefault();

            if (matchingCoupon == null)
            {
                throw new ArgumentException($"There is no matching Coupon of {couponValue}. Please delete your coupon value if you don't want to use coupon.");
            }

            var currentTime = DateTime.Now;
         

            if (currentTime < matchingCoupon.StartDate || currentTime > matchingCoupon.EndDate)
            {
              
                throw new ArgumentException($"{matchingCoupon.CouponIDValue} has expired. Please provide another coupon.");

            }

            return _salesContext.Coupons.Where(x => x.CouponIDValue == couponValue)
                                       .Select(x => x.CouponDiscount).FirstOrDefault();

        }




    }
}
