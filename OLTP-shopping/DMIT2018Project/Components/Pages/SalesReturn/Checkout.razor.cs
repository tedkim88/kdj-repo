using Microsoft.AspNetCore.Components;
using SalesSystem;
using SalesSystem.BLL;
using SalesSystem.ViewModels;
using SalesSystem.Entities;
using static MudBlazor.Colors;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DMIT2018Project.Components.Pages.SalesReturn
{
    public partial class Checkout
    {
        private string feedbackMessage = "";
        private List<string> errorDetails = new();
        private string errorMessage = "";


        private int saleID = 0;
        private int employeeID = 1;
        private string couponInput = "";
        private string paymentMethod = "";
        private int discount = 0;
        private List<string> paymentMethods = new() { "Money", "Credit", "Debit" };
        //private Employee currentEmployee = new();


        private decimal subtotal => AppState.cartView.Sum(a => a.Subtotal);
        private decimal discountedSubtotal => AppState.cartView.Sum(a => a.Subtotal) * (100 - discount) / 100;
        private decimal tax => subtotal * 0.05m;
        private decimal discountedTax => discountedSubtotal * 0.05m;
        private decimal total => subtotal + tax;
        private decimal discountedTotal => discountedSubtotal + discountedTax;





        [Inject]
        protected AppState AppState { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected CouponService CouponService { get; set; }

        [Inject]
        protected SaleService SaleService { get; set; }


        protected override void OnInitialized()
        {
            if (AppState.cartView.Count == 0)
            {
                errorMessage = "No Checkout Items yet.";
            }

            base.OnInitialized();
        }


        private void MovetoCart()
        {
            NavigationManager.NavigateTo("/SalesReturn/Cart");
        }


        private void HandlePaymentSelection(ChangeEventArgs e)
        {
            paymentMethod = e.Value.ToString();
        }

        private void CouponCheck()
        {
            feedbackMessage = "";
            errorMessage = "";
            errorDetails.Clear();

            try
            {
                discount = CouponService.GetCouponRate(couponInput);
                feedbackMessage = $"Discount Applied! {discount} percent.";
            }

            catch (AggregateException ex)
            {
                foreach (var error in ex.InnerExceptions)
                {
                    
                    errorDetails.Add(error.Message);
                }
                discount = 0;
            }
            catch (ArgumentNullException ex)
            {
                errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
                discount = 0;
            }
            catch (Exception ex)
            {
                errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
                discount = 0;
            }
        }

        private void Save()
        {
            feedbackMessage = "";
            errorMessage = "";
            errorDetails.Clear();
            try
            {

                if (AppState.cartView.Count == 0)
                {
                    errorMessage = "Can't place order when the cart is empty.";
                }
                else
                {
                    int newSaleID = SaleService.AddSales(AppState, employeeID, couponInput, paymentMethod, saleID);
                    feedbackMessage = $"Success--your new saleID is {newSaleID}.";
                }
            }
            catch (AggregateException ex)
            {
                foreach (var error in ex.InnerExceptions)
                {
                    errorDetails.Add(error.Message);
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
