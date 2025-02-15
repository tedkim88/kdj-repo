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
        /// <summary>
        /// The Sales context
        /// 
        /// </summary>
        private readonly SalesContext _salesContext;

        #endregion

        internal CouponService(SalesContext salesContext)
        {
            //Initialize the _salesContext field with the provided HogWildContext instance
            _salesContext = salesContext;

        }

        public int GetCouponRate(string couponValue)
        {
            if(string.IsNullOrWhiteSpace(couponValue))
            {
                throw new ArgumentException("Please enter your coupon for validation.(case-sensitive)");
            }

            var matchingCoupon = _salesContext.Coupons
                                   .AsEnumerable() // Bring data into memory for client-side evaluation
                                   .Where(x => x.CouponIDValue.Equals(couponValue, StringComparison.Ordinal))
                                   .FirstOrDefault();

            if (matchingCoupon == null)
            {
                throw new ArgumentException($"There is no matching Coupon of {couponValue}. Please delete your coupon value if you don't want to use coupon.");
            }

            var currentTime = DateTime.Now;
            //bool CouponExpired = false;

            if (currentTime < matchingCoupon.StartDate || currentTime > matchingCoupon.EndDate)
            {
                //CouponExpired = true;
                throw new ArgumentException($"{matchingCoupon.CouponIDValue} has expired. Please provide another coupon.");

            }

            return _salesContext.Coupons.Where(x => x.CouponIDValue == couponValue)
                                       .Select(x => x.CouponDiscount).FirstOrDefault();

        }




    }
}
