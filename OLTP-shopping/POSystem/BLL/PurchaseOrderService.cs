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
    public class PurchaseOrderService
    {
        #region Fields

        private readonly POContext _purchaseOrderContext;

        #endregion

        internal PurchaseOrderService(POContext purchaseOrderContext)
        {
            _purchaseOrderContext = purchaseOrderContext;
        }

        public PurchaseOrderView GetActiveOrderForVendor(int vendorID)
        {
            if (vendorID <= 0)
            {
                throw new ArgumentNullException("Invalid vendor ID provided.");
            }

            // Fetch purchase order
            var purchaseOrder = _purchaseOrderContext.PurchaseOrders
                .Where(x => x.VendorID == vendorID && x.OrderDate == null && !x.RemoveFromViewFlag)
                .Select(x => new PurchaseOrderView
                {
                    PurchaseOrderID = x.PurchaseOrderID,
                    OrderDate = x.OrderDate,
                    Vendor = x.Vendor.VendorName,
                    Phone = x.Vendor.Phone,
                    Address = x.Vendor.Address,
                    PostalCode = x.Vendor.PostalCode,
                    City = x.Vendor.City,
                    PurchaseOrderDetails = x.PurchaseOrderDetails
                        .Where(pod => !pod.RemoveFromViewFlag && pod.StockItem.VendorID == vendorID)
                        .Select(pod => new PurchaseOrderDetailView
                        {
                            PurchaseOrderDetailID = pod.PurchaseOrderDetailID,
                            StockItemID = pod.StockItemID,
                            Description = pod.StockItem.Description,
                            QuantityOnHand = pod.StockItem.QuantityOnHand,
                            ReorderLevel = pod.StockItem.ReOrderLevel,
                            QuantityOnOrder = pod.StockItem.QuantityOnOrder,
                            QuantityToOrder = pod.Quantity,
                            Price = pod.PurchasePrice,
                            OriginalPrice = pod.StockItem.SellingPrice,
                            RemoveFromViewFlag = pod.RemoveFromViewFlag
                        }).ToList()
                })
                .FirstOrDefault();

            // Validation: Check for invalid purchase order details
            if (purchaseOrder != null)
            {
                var errorList = new List<Exception>();

                foreach (var detail in purchaseOrder.PurchaseOrderDetails)
                {
                    if (detail.QuantityToOrder <= 0)
                    {
                        errorList.Add(new Exception($"Quantity to order for StockItemID {detail.StockItemID} must be greater than 0."));
                    }

                    if (detail.Price < 0)
                    {
                        errorList.Add(new Exception($"Price for StockItemID {detail.StockItemID} cannot be less than 0."));
                    }
                }

                // If there are validation errors, throw an aggregate exception
                if (errorList.Any())
                {
                    throw new AggregateException("Validation errors occurred in the purchase order details.", errorList);
                }

                // Calculate totals if validation passes
                purchaseOrder.SubTotal = purchaseOrder.PurchaseOrderDetails.Sum(detail => detail.QuantityToOrder * detail.Price);
                purchaseOrder.GST = purchaseOrder.SubTotal * 0.05m;
                purchaseOrder.Total = purchaseOrder.SubTotal + purchaseOrder.GST;
            }

            return purchaseOrder;
        }

        public void UpdateOrder(PurchaseOrderView purchaseOrderView)
        {
            if (purchaseOrderView == null)
            {
                throw new ArgumentNullException(nameof(purchaseOrderView), "Purchase order data is required.");
            }

            var purchaseOrder = _purchaseOrderContext.PurchaseOrders
                .FirstOrDefault(x => x.PurchaseOrderID == purchaseOrderView.PurchaseOrderID && !x.RemoveFromViewFlag);

            if (purchaseOrder == null)
            {
                throw new KeyNotFoundException($"Purchase Order ID {purchaseOrderView.PurchaseOrderID} not found.");
            }

            foreach (var detailView in purchaseOrderView.PurchaseOrderDetails)
            {
                var detail = _purchaseOrderContext.PurchaseOrderDetails
                    .FirstOrDefault(x => x.PurchaseOrderDetailID == detailView.PurchaseOrderDetailID);

                if (detail == null)
                {
                    throw new KeyNotFoundException($"Purchase Order Detail ID: {detailView.PurchaseOrderDetailID} not found.");
                }

                detail.Quantity = detailView.QuantityToOrder;
                detail.PurchasePrice = detailView.Price;
            }

            _purchaseOrderContext.SaveChanges();
        }

        public void DeleteOrder(int purchaseOrderID)
        {
            var purchaseOrder = _purchaseOrderContext.PurchaseOrders
                .FirstOrDefault(x => x.PurchaseOrderID == purchaseOrderID && !x.RemoveFromViewFlag);

            if (purchaseOrder == null)
            {
                throw new KeyNotFoundException($"Purchase Order ID: {purchaseOrderID} not found.");
            }

            purchaseOrder.RemoveFromViewFlag = true;

            _purchaseOrderContext.SaveChanges();
        }


    }
}
