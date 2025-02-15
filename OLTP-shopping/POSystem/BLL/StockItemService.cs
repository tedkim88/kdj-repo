using POSystem.DAL;
using POSystem.Entities;
using POSystem.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace POSystem.BLL
{
    public class StockItemService
    {
        #region Fields

        private readonly POContext _purchaseOrderContext;

        #endregion

        internal StockItemService(POContext purchaseOrderContext)
        {
            _purchaseOrderContext = purchaseOrderContext;
        }

        // Fetch Stock Items By Vendor
        public List<StockItemView> GetStockItemsForVendor(int vendorID, List<int> existingStockItemIDs)
        {
            return _purchaseOrderContext.StockItems
                .Where(x => x.VendorID == vendorID &&
                               !x.RemoveFromViewFlag &&
                               !existingStockItemIDs.Contains(x.StockItemID))
                .Select(x => new StockItemView
                {
                    StockItemID = x.StockItemID,
                    Description = x.Description,
                    QuantityOnHand = x.QuantityOnHand,
                    ReorderLevel = x.ReOrderLevel,
                    Price = x.PurchasePrice,
                    RemoveFromViewFlag = x.RemoveFromViewFlag
                })
                .OrderBy(x => x.Description)
                .ToList();
        }
    }
}
