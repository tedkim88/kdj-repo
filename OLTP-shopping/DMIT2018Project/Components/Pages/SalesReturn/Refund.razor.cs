using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SalesSystem.BLL;
using SalesSystem.ViewModels;

namespace DMIT2018Project.Components.Pages.SalesReturn
{
    public partial class Refund
    {

        private int employeeID = 1;
        private string errorMessage = "";
        List<string> errorDetails = new();
        private string feedbackMessage = "";
        private int saleID = 0;

        private bool clickChangeQty = false;

        [Inject]
        protected SaleService SaleService { get; set; }

        [Inject]
        protected CouponService CouponService { get; set; }

        private List<ReturnItemView> refundList = new();
        private List<ReturnItemView> cloneforComparison = new();


        private string dialogMessage = "";
        private bool showDialog = false;
        private TaskCompletionSource<bool?> dialogCompletionSource;


        private decimal subTotalOfReturnItems = 0;
        private decimal taxOfReturnItems = 0;
        private decimal totalReturn = 0;




        public void GetSale()
        {
            feedbackMessage = "";
            errorMessage = "";
            errorDetails.Clear();
            subTotalOfReturnItems = 0;
            taxOfReturnItems = 0;
            totalReturn = 0;
            try
            {
                if (saleID <= 0)
                {
                    throw new ArgumentNullException("Invalid--SaleID should be greater than 0.");
                }

                refundList = SaleService.GetSaleRefund(saleID);

                //getting a deep copy from database to maintain the initial data. 
                //if clone references refundList, that's a shallow copy and clone data changes when refundList changes, which I don't want to happen.
                cloneforComparison = SaleService.GetSaleRefund(saleID);

            }
            catch (AggregateException ex)
            {
                errorMessage = ex.Message;
                foreach (var innerException in ex.InnerExceptions)
                {
                    errorDetails.Add(innerException.Message);
                }
            }
            catch (ArgumentNullException ex)
            {
                errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }
            catch (Exception ex)
            {
                errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }

        }


        private async Task SimpleDialogResult(bool? result)
        {
            showDialog = false; // Hide the dialog after the user interaction
            dialogCompletionSource.SetResult(result);
            await InvokeAsync(StateHasChanged); // Ensure the UI updates to hide the dialog
        }


        private async Task<bool?> ShowDialogAsync()
        {
            dialogCompletionSource = new TaskCompletionSource<bool?>();
            showDialog = true; // Show the dialog
            await InvokeAsync(StateHasChanged); // Ensure the UI updates to show the dialog
            return await dialogCompletionSource.Task;
        }

        public async Task Clear()
        {
            dialogMessage = "Do you wish to close the rental editor?";
            bool? result = await ShowDialogAsync();

            if (result == true)
            {

                saleID = 0;
                feedbackMessage = "";
                refundList.Clear();

                errorMessage = "";
                errorDetails.Clear();
            }
        }


        private void AddSaleRefund()
        {

            feedbackMessage = "";
            errorMessage = "";
            errorDetails.Clear();


            try
            {

                if (refundList.Count == 0)
                {
                    throw new ArgumentNullException("Enter Valid SaleID and Submit first.");
                }

                if (refundList.Any(a => a.qtyPlaceholder < 0))
                {
                    errorDetails.Add("Negative number for return is invalid.");
                }

                

                foreach (var item in refundList)
                {
                    var matching = cloneforComparison.Where(a => a.StockItemID == item.StockItemID).FirstOrDefault();
                    if (matching != null)
                    {
                        if (item.qtyPlaceholder > matching.AvailQTY)
                        {
                            errorDetails.Add($"(Item):{item.Item} : Return Quantity cannot go over Avail Qty.");
                        }

                        if((item.qtyPlaceholder+item.RtnQty > matching.AvailQTY) && (item.RtnQty!=0))
                        {
                            errorDetails.Add($"(Item):{item.Item} : QtyToRtn + Returned(past) quantity cannot go over Avail Qty. Your maximum Return amount for this item is {matching.AvailQTY - item.RtnQty}." );
                        }
                    }

                    

                }


                if (!(refundList.Any(a => a.qtyPlaceholder != 0)))
                {
                    errorDetails.Add("At least one item should be returned to proceed.");
                }

                if (!clickChangeQty && errorDetails.Count == 0)
                {
                    errorDetails.Add("Click Change Qty button to reflect the return quantity item.");
                }

                if (errorDetails.Count == 0)
                {
                    int newRefundID = SaleService.AddSaleRefund(refundList, employeeID);
                    refundList = SaleService.GetSaleRefund(refundList.Select(x=>x.SaleID).FirstOrDefault());
                    feedbackMessage = $"Success - your new refundID is {newRefundID}.";
                }


            }
            catch (AggregateException ex)
            {
                errorMessage = "Failed to add sale refund";
                foreach (var innerException in ex.InnerExceptions)
                {
                    errorDetails.Add(innerException.Message);
                }
            }
            catch (ArgumentNullException ex)
            {
                errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }
            catch (Exception ex)
            {
                errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }

        }




        //I didn't use editContext for dynamic validation here, since I used it in the cart page already.
        //Just wanted to check if my change qty button would work the way I expected, similarly to the way it was described in the instruction on brightspace. 
        public void EditReturnQty(int rtnqty, int stockItemID)
        {
            feedbackMessage = "";
            errorMessage = "";
            errorDetails.Clear();

            try
            {
                //create a list<Exception> to contain all discovered errors
                List<Exception> errorList = new List<Exception>();

                //selecting item which is to be edited
                var matchingItem = refundList
                                            .Where(x => x.StockItemID == stockItemID)
                                            .FirstOrDefault();

                //this is a sort of constant variable. to maintain the initial availqty for comparison.
                var matchingCloneItem = cloneforComparison.Where(x => x.StockItemID == stockItemID)
                                            .FirstOrDefault();

                

                if (matchingItem == null)
                {
                    errorList.Add(new Exception("there is no matching Item with that name."));
                }



                //can't return more than bought

                if (rtnqty > matchingCloneItem.AvailQTY)
                {
                    //matchingItem.qtyPlaceholder = matchingCloneItem.AvailQTY;
                    //EditReturnQty(matchingCloneItem.AvailQTY, stockItemID);
                    //errorList.Add(new Exception("You can't return more than you have bought for this item. Modified to maximum."));
                    errorList.Add(new Exception("You can't return more than you have bought for this item."));
                }

                
                if (rtnqty < 0)
                {
                    //matchingItem.qtyPlaceholder = 0;
                    //EditReturnQty(0, stockItemID);
                    //errorList.Add(new Exception("Quantity can't be a negative number. Modified to 0."));
                    errorList.Add(new Exception("Quantity can't be a negative number."));
                }

                if(matchingItem.qtyPlaceholder + matchingItem.RtnQty >matchingCloneItem.AvailQTY)
                {
                    errorList.Add(new Exception($"Returned Quantity + QtyToReturn can't go over the original sale amount {matchingCloneItem.AvailQTY}."));
                }




                if (errorList.Count > 0)
                {
                    //Clearing the "track changes" ensures consistency in our entity system.
                    //Otherwise we leave our entity system in flux
                    //ChangeTracker.Clear();
                    //Throw an AggregateException containing the list of business processing errors.
                    throw new AggregateException("Failed to edit return quantity", errorList);
                }

                //Users may continuously change return quantity. It may increase, or decrease. Availqty should also change accordingly.

                //when increasing rtn qty from the previous data
                if (rtnqty > matchingItem.QtyToRtn)
                {
                    var difference = rtnqty - matchingItem.QtyToRtn;
                    matchingItem.QtyToRtn = rtnqty;
                    matchingItem.AvailQTY -= difference;
                }

                //when decreasing rtn qty from the previous data
                if (rtnqty < matchingItem.QtyToRtn)
                {
                    var difference = matchingItem.QtyToRtn - rtnqty;
                    matchingItem.QtyToRtn = rtnqty;
                    matchingItem.AvailQTY += difference;
                }

                //the title is original price(according to the sample). But it is the subtotal of the specific item that customer still has in his hand.
                matchingItem.OriginalPrice = matchingItem.AvailQTY * matchingItem.SellingUnitPrice;

                feedbackMessage = "Quantity has been successfully changed.";

                subTotalOfReturnItems = refundList.Sum(a => a.QtyToRtn * a.SellingUnitPrice);

                taxOfReturnItems = subTotalOfReturnItems * 0.05m;
                //this is tax calculation before discount is applied.
                //I'll include discount for tax as well in 'totalReturn' for customer.
                //but in database just decided not to reflect discount. talked about this with James.

                var matchingCouponRate = refundList.Select(a => a.discountRate).FirstOrDefault();

                if (matchingCouponRate == null)
                {
                    throw new ArgumentException("there is no matching CouponRate");
                }
                totalReturn = subTotalOfReturnItems * (100 - matchingCouponRate) / 100 + taxOfReturnItems * (100 - matchingCouponRate) / 100;
                clickChangeQty = true;
                //totalReturn is for customer and it is applying discount to tax as well. 

                //checking the result in Linqpad.


                //Console.WriteLine($"Your total return is {totalReturn} including tax.");

            }
            catch (AggregateException ex)
            {
                errorMessage = "Failed to edit return quantity";
                foreach (var innerException in ex.InnerExceptions)
                {
                    errorDetails.Add(innerException.Message);
                }
            }
            catch (ArgumentNullException ex)
            {
                errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }
            catch (Exception ex)
            {
                errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }

        }
    }

}
