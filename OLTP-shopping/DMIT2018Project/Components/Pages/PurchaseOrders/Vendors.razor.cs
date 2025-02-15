using POSystem.BLL;
using Microsoft.AspNetCore.Components;
using POSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMIT2018Project.Components.Pages.PurchaseOrders
{
    public partial class Vendors
    {
        #region Fields
        private string feedback;
        private VendorView vendorView = new VendorView();
        private int vendorID;
        private PurchaseOrderView? purchaseOrderView;
        private List<StockItemView> inventory = new();
        public required List<VendorView> vendorList;
        private string errorMessage;
        private bool hasFeedback => !string.IsNullOrWhiteSpace(feedback);
        private bool hasError => !string.IsNullOrWhiteSpace(errorMessage);
        private List<string> errorDetails = new();

        private bool showDialog = false;
        private string dialogMessage = string.Empty;
        private TaskCompletionSource<bool?> dialogCompletionSource;
        #endregion

        #region Properties
        [Inject]
        protected VendorService VendorService { get; set; }
        [Inject]
        protected PurchaseOrderService PurchaseOrderService { get; set; }
        [Inject]
        protected StockItemService StockItemService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        #endregion

        #region Methods

        private async Task OnVendorSelected(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value?.ToString(), out int selectedVendorID) && selectedVendorID > 0)
            {
                vendorID = selectedVendorID;

                try
                {
                    feedback = "";
                    errorMessage = "";
                    errorDetails.Clear();

                    vendorView = VendorService.GetVendors().FirstOrDefault(x => x.VendorID == vendorID);

                    purchaseOrderView = await Task.Run(() =>
                        PurchaseOrderService.GetActiveOrderForVendor(vendorID));

                    if (purchaseOrderView == null)
                    {
                        feedback = "No active purchase order found for the selected vendor.";
                        inventory = StockItemService.GetStockItemsForVendor(vendorID, new List<int>());
                    }
                    else
                    {
                        var existingIDs = purchaseOrderView.PurchaseOrderDetails.Select(d => d.StockItemID).ToList();
                        inventory = StockItemService.GetStockItemsForVendor(vendorID, existingIDs);
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Error: {ex.Message}";
                    errorDetails.Add(ex.Message);
                }
            }
            else
            {
                feedback = "Please select a valid vendor.";
                purchaseOrderView = null;
                inventory.Clear();
            }
        }

        private async Task AddToPurchaseOrder(StockItemView item)
        {
            if (purchaseOrderView == null) return;

            var newDetail = new PurchaseOrderDetailView
            {
                StockItemID = item.StockItemID,
                Description = item.Description,
                QuantityToOrder = 1,
                Price = item.Price,
                OriginalPrice = item.Price
            };

            purchaseOrderView.PurchaseOrderDetails.Add(newDetail);
            inventory.Remove(item);
            UpdatePurchaseOrderTotals();
        }

        private async Task RemoveFromPurchaseOrder(PurchaseOrderDetailView detail)
        {
            if (purchaseOrderView == null) return;

            var item = new StockItemView
            {
                StockItemID = detail.StockItemID,
                Description = detail.Description,
                QuantityOnHand = detail.QuantityOnHand,
                ReorderLevel = detail.ReorderLevel,
                Price = detail.Price
            };

            inventory.Add(item);
            purchaseOrderView.PurchaseOrderDetails.Remove(detail);
            UpdatePurchaseOrderTotals();
        }

        private async Task<bool?> ShowDialogAsync()
        {
            dialogCompletionSource = new TaskCompletionSource<bool?>();
            return await dialogCompletionSource.Task;
        }

        private void SimpleDialogResult(bool? result)
        {
            showDialog = false;
            dialogCompletionSource.SetResult(result);
        }

        private async Task ShowUpdateDialog()
        {
            dialogMessage = "Are you sure you want to update the order?";
            showDialog = true;

            bool? result = await ShowDialogAsync();
            if (result == true)
            {
                UpdateOrder();
            }
        }

        private async Task ShowPlaceDialog()
        {
            dialogMessage = "Are you sure you want to place the order?";
            showDialog = true;

            bool? result = await ShowDialogAsync();
            if (result == true)
            {
                PlaceOrder();
            }
        }

        private async Task ShowDeleteDialog()
        {
            dialogMessage = "Are you sure you want to delete the order?";
            showDialog = true;

            bool? result = await ShowDialogAsync();
            if (result == true)
            {
                DeleteOrder();
            }
        }

        private async Task ShowClearDialog()
        {
            dialogMessage = "Are you sure you want to clear the order?";
            showDialog = true;

            bool? result = await ShowDialogAsync();
            if (result == true)
            {
                await ClearOrder();
            }
        }

        private async Task ShowCancelDialog()
        {
            dialogMessage = "Are you sure you want to cancel?";
            showDialog = true;

            bool? result = await ShowDialogAsync();
            if (result == true)
            {
                CancelOrder();
            }
        }

        private void UpdateOrder()
        {
            if (purchaseOrderView == null) return;

            try
            {
                PurchaseOrderService.UpdateOrder(purchaseOrderView);
                feedback = "Purchase order updated successfully.";
                errorMessage = "";
                errorDetails.Clear();
            }
            catch (Exception ex)
            {
                errorMessage = $"Error updating purchase order: {ex.Message}";
                errorDetails.Add(ex.Message);
            }
        }

        private void PlaceOrder()
        {
            feedback = "Place Order button clicked.";
        }

        private void DeleteOrder()
        {
            if (purchaseOrderView == null) return;

            try
            {
                PurchaseOrderService.DeleteOrder(purchaseOrderView.PurchaseOrderID);
                feedback = "Purchase order deleted successfully.";
                errorMessage = "";
                errorDetails.Clear();

                purchaseOrderView = null;
                inventory.Clear();
            }
            catch (Exception ex)
            {
                errorMessage = $"Error deleting purchase order: {ex.Message}";
                errorDetails.Add(ex.Message);
            }
        }


        private async Task ClearOrder()
        {
            if (vendorID > 0)
            {
                try
                {
                    purchaseOrderView = await Task.Run(() =>
                        PurchaseOrderService.GetActiveOrderForVendor(vendorID));

                    if (purchaseOrderView != null)
                    {
                        var existingIDs = purchaseOrderView.PurchaseOrderDetails
                            .Select(d => d.StockItemID)
                            .ToList();

                        inventory = StockItemService.GetStockItemsForVendor(vendorID, existingIDs);

                        feedback = "Order cleared! All changes have been reverted.";
                    }
                    else
                    {
                        feedback = "No active purchase order to clear.";
                    }

                    errorMessage = "";
                    errorDetails.Clear();

                    UpdatePurchaseOrderTotals();
                }
                catch (Exception ex)
                {
                    errorMessage = $"Error clearing the order: {ex.Message}";
                    errorDetails.Add(ex.Message);
                }
            }
            else
            {
                feedback = "No vendor selected to clear the order.";
                purchaseOrderView = null;
                inventory.Clear();
            }
        }


        private void CancelOrder()
        {
            NavigationManager.NavigateTo("/");
        }

        private void OnQuantityOrPriceChanged(PurchaseOrderDetailView detail, string newValue, bool isQuantity)
        {
            try
            {
                decimal parsedValue = 0;
                bool isValidDecimal = false;

                try
                {
                    parsedValue = Convert.ToDecimal(newValue);
                    isValidDecimal = true;
                }
                catch
                {
                    isValidDecimal = false;
                }

                if (!isValidDecimal)
                {
                    feedback = isQuantity
                        ? "Invalid input. Please enter a valid number for quantity."
                        : "Invalid input. Please enter a valid number for price.";
                    errorMessage = "Validation error.";
                    if (!errorDetails.Contains(feedback))
                    {
                        errorDetails.Add(feedback);
                    }
                    return;
                }

                if (isQuantity)
                {
                    if (parsedValue > 0)
                    {
                        detail.QuantityToOrder = (int)parsedValue;
                        feedback = "";
                        errorMessage = "";
                        errorDetails.Clear();
                    }
                    else
                    {
                        feedback = "Quantity must be greater than 0.";
                        errorMessage = "Validation error.";
                        if (!errorDetails.Contains(feedback))
                        {
                            errorDetails.Add(feedback);
                        }
                        return;
                    }
                }
                else
                {
                    if (parsedValue >= 0)
                    {
                        detail.Price = parsedValue;
                        feedback = "";
                        errorMessage = "";
                        errorDetails.Clear();
                    }
                    else
                    {
                        feedback = "Price cannot be negative.";
                        errorMessage = "Validation error.";
                        if (!errorDetails.Contains(feedback))
                        {
                            errorDetails.Add(feedback);
                        }
                        return;
                    }
                }

                UpdatePurchaseOrderTotals();
            }
            catch (Exception ex)
            {
                errorMessage = "An unexpected error occurred.";
                errorDetails.Add(ex.Message);
            }
        }

        private void UpdatePurchaseOrderTotals()
        {
            if (purchaseOrderView == null) return;

            purchaseOrderView.SubTotal = purchaseOrderView.PurchaseOrderDetails.Sum(d => d.QuantityToOrder * d.Price);
            purchaseOrderView.GST = purchaseOrderView.SubTotal * 0.05m;
            purchaseOrderView.Total = purchaseOrderView.SubTotal + purchaseOrderView.GST;
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            try
            {
                vendorList = VendorService.GetVendors();
                if (!vendorList.Any())
                {
                    feedback = "No vendors available.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Error: {ex.Message}";
                errorDetails.Add(ex.Message);
            }
        }

        #endregion
    }
}
