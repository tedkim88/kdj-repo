using Microsoft.AspNetCore.Components;
using SalesSystem;
using SalesSystem.BLL;
using SalesSystem.Entities;
using SalesSystem.ViewModels;
using System.ComponentModel;

namespace DMIT2018Project.Components.Pages.SalesReturn
{
    public partial class Shopping
    {
        //getting categoryID from user for selecting
        private int? categoryID = null;

        //the feedback message
        private string feedbackMessage = "";
        private string dialogMessage = "";
        private bool showDialog = false;

        //the error message
        private string errorMessage = "";
        private TaskCompletionSource<bool?> dialogCompletionSource;
        private List<ShoppingItemsView> cartItems = new List<ShoppingItemsView>();

        private List<CategoryView> categories = new();
        private List<StockItemView> stockItems = new();

        private int countOfAllItems => temporaryHolder.Count();
        private List<StockItemView> temporaryHolder = new();
        private List<string> errorDetails = new();

        [Inject]
        protected AppState AppState { get; set; }

        [Inject]
        protected CategoryService CategoryService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            GetCategories();
            temporaryHolder = CategoryService.GetStockItems();
            base.OnInitialized();

            //카트페이지에서 다시 쇼핑하기위해 돌아왔을때(즉 appstate가 차있을때)
            if (AppState.cartView.Count > 0)
            {
                cartItems = AppState.cartView; //this is for UI. so that cartitems (from the previous cart) is shown on UI.
            }

        }

        private void GetCategories()
        {
            errorDetails.Clear();
            errorMessage = string.Empty;
            feedbackMessage = string.Empty;

            try
            {
                categories = CategoryService.GetCategories(); // Call the service to fetch categories

                if (categories == null || !categories.Any())
                {
                    feedbackMessage = "No categories available.";
                }

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

        private void GetItems(int? categoryID)
        {
            try
            {
                if (categoryID == null)
                {
                    stockItems.Clear();
                }
                else if (categoryID == 0)
                {
                    stockItems = CategoryService.GetStockItems();
                }
                else
                {
                    stockItems = CategoryService.GetItemsByCategoryID(categoryID);
                }
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


        private void AddToShoppingList(int productID, int qty)
        {
            feedbackMessage = "";
            errorMessage = "";

            if (qty <= 0)
            {
                errorMessage = "Invalid Quantity. Qty should be greater than 0 to add to cart.";
            }
            else
            {
                try
                {


                    var existingItem = cartItems.Where(x => x.ProductID == productID).FirstOrDefault();


                    if (existingItem != null)
                    {

                        //Console.WriteLine("Duplicate item is already in the cart! quantity added to the existing Item");
                        existingItem.Quantity += qty;

                    }
                    else
                    {
                        cartItems.Add(new ShoppingItemsView
                        {
                            ProductID = productID,
                            ProductName = stockItems.Where(x => x.StockItemID == productID).Select(x => x.Description).FirstOrDefault(),

                            Quantity = qty,
                            UnitPrice = stockItems.Where(x => x.StockItemID == productID).Select(x => x.SellingPrice).FirstOrDefault(),
                        });
                    }

                    feedbackMessage = "Item has been added to the cart.";
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

        }

        private void MoveToCart()
        {

            if (cartItems.Count > 0)
            {
                AppState.cartView = cartItems; //쇼핑리스트 추가했다가(카트페이지갔다가) 다시 컨티뉴쇼핑으로 여기로 돌아와서 다시 카트를 누르는순간 새로생성된 count0인 cartitem에 앱스테이트에 할당되는게 문제.!!
            }
            NavigationManager.NavigateTo("/SalesReturn/Cart");
        }





        private async Task Clear()
        {

            dialogMessage = "Do you wish to close?";
            bool? result = await ShowDialogAsync();

            if (result == true)
            {
                categoryID = null;
                stockItems.Clear();
                feedbackMessage = "";
                errorMessage = "";
                cartItems.Clear();
                errorDetails.Clear();
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


        ///This was my approach when I didn't know how to use Appstate. 
        ///To share the values or data across different pages, I thought I needed to make a public method to return the data that I need to share.
        ///This might work too, but..somehow moving between pages, data ended up being lost. so I switched to using Appstate. 
        
        //public List<ShoppingItemsView> GetCartItems()
        //{
        
        //    return cartItems;
        //}








    }

}
