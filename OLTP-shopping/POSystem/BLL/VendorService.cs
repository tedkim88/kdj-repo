using POSystem.DAL;
using POSystem.Entities;
using POSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSystem.BLL
{
    public class VendorService
    {
        #region Fields

        private readonly POContext _purchaseOrderContext;

        #endregion

        internal VendorService(POContext purchaseOrderContext)
        {
            _purchaseOrderContext = purchaseOrderContext;
        }


        // Fetch Vendors
        public List<VendorView> GetVendors()
        {
            return _purchaseOrderContext.Vendors
                    .Where(x => x.RemoveFromViewFlag == false)
                    .Select(x => new VendorView
                    {
                        VendorID = x.VendorID,
                        VendorName = x.VendorName,
                        Address = x.Address
                    })
                    .OrderBy(x => x.VendorName)
                    .ToList();
        }
    }
}
