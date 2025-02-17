using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SalesSystem;
using SalesSystem.BLL;
using SalesSystem.ViewModels;

namespace DMIT2018Project.Components.Pages.SalesReturn
{
    public partial class Cart
    {

        private string feedbackMessage = "";



        private EditContext editContext;
        private ValidationMessageStore messageStore;



        //if I choose this way, this is a new categoryservice object. which is different form shopping page.
        //so I can't obtain the original shopping list passed from shopping page. because they are two different service objects.
        //So I need to use AppState(is it like a global variable that can be used somewhere else? maybe.. need to learn this further)
        //[Inject]
        //protected CategoryService CategoryService { get; set; }
        [Inject]
        AppState AppState { get; set; }


        [Inject]
        protected NavigationManager NavigationManager { get; set; }


        [Inject]
        protected CategoryService CategoryService { get; set; }

        private List<ShoppingItemsView> shoppingCart = new List<ShoppingItemsView>();
        private List<StockItemView> stocks = new List<StockItemView>();



        public decimal subtotal => AppState.cartView.Sum(a => a.Subtotal);
        public decimal tax => subtotal * 0.05m;
        public decimal total => subtotal + tax;







        protected override void OnInitialized()
        {

            //  edit context needs to be setup after data has been initialized
            //  setup of the edit context to make use of the payment type property
            editContext = new EditContext(shoppingCart);

            //  set the validation to use the HandleValidationRequest event
            editContext.OnValidationRequested += HandleValidationRequested;

            //  setup the message store to track any validation messages
            messageStore = new ValidationMessageStore(editContext);

            //  this event will fire each time the data in a property has change.
            editContext.OnFieldChanged += EditContext_OnFieldChanged;



            if (AppState.cartView.Count > 0)
            {
                shoppingCart = AppState.cartView; //for UI
                //I found Appstate can be used across all pages sharing data, but
                //using shopping cart was my first approach(making it public method so that I can share across different pages)
                //so decided to leave it undeleted for learning purpose.
            }
            else
            {
                feedbackMessage = "Shopping Cart is Empty.";
            }

            //stocks = CategoryService.GetStockItems();
            base.OnInitialized();
        }



        private void MovetoShopping()
        {
            NavigationManager.NavigateTo("/SalesReturn/Shopping/");
        }

        private void MovetoCheckout()
        {

            NavigationManager.NavigateTo("/SalesReturn/Checkout/");
        }


        private void HandleValidationRequested(object sender, ValidationRequestedEventArgs e)
        {
            // Clear the message store to remove any existing validation errors.
            messageStore?.Clear();

            // Iterate through each invoice line to perform custom validations.
            foreach (var item in shoppingCart)
            {
                // Custom validation: Ensure Quantity is greater than 0.
                if (item.Quantity <= 0)
                {
                    // Add a validation message for the Quantity field.
                    messageStore.Add(new FieldIdentifier(item, nameof(item.Quantity)),
                        "Quantity must be greater than 0.");
                }


                //var productInDatabase = stocks.Where(x => x.StockItemID == item.ProductID).FirstOrDefault();

                //if (productInDatabase != null)
                //{
                //    if (item.Quantity > productInDatabase.QuantityOnHand)
                //    {
                //        // Add a validation message for the Quantity field.
                //        messageStore.Add(new FieldIdentifier(item, nameof(item.Quantity)),
                //            $"Quantity can't be more than Quantity on Hand :{productInDatabase.QuantityOnHand}.");
                //    }
                //}

                // Custom validation: Ensure Price is greater than 0.

            }

            // Notify the EditContext that the validation state has changed.
            editContext.NotifyValidationStateChanged();

            // Trigger UI updates to reflect the validation messages.
            InvokeAsync(StateHasChanged);
        }


        private void EditContext_OnFieldChanged(object sender, FieldChangedEventArgs e)
        {
            // Clear previous validation messages for the specific field that changed.
            messageStore?.Clear(e.FieldIdentifier);

            // Perform custom validation for Quantity.
            // Check if the field belongs to an InvoiceLineView object using the `is` operator with pattern matching.
            // If true, the `line` variable is assigned as a strongly-typed reference to the Model.
            // Then, verify that the field being validated is the Quantity field.
            if (e.FieldIdentifier.Model is ShoppingItemsView item && e.FieldIdentifier.FieldName == nameof(item.Quantity))
            {
                if (item.Quantity <= 0)
                {
                    // Add a validation message for the Quantity field.
                    messageStore?.Add(e.FieldIdentifier, "Quantity must be greater than 0.");
                }
            }

            // Perform custom validation for Price.
            // Check if the field belongs to an InvoiceLineView object using the `is` operator with pattern matching.
            // If true, the `line2` variable is assigned as a strongly-typed reference to the Model.
            // Then, verify that the field being validated is the Price field.


            // Notify the EditContext that the validation state has changed.
            editContext.NotifyValidationStateChanged();

            // Trigger UI updates to reflect the validation messages.
            InvokeAsync(StateHasChanged);
        }

        private void NotifyFieldChanged(ShoppingItemsView line, string propertyName)
        {
            // Create a FieldIdentifier for the specified field.
            var fieldIdentifier = new FieldIdentifier(line, propertyName);

            // Notify the EditContext that the field has changed.
            editContext.NotifyFieldChanged(fieldIdentifier);

            // Optionally trigger validation for the entire form (if needed).
            editContext.Validate();
        }


        private void SyncQuantity(object? inputValue, ShoppingItemsView item)
        {
            // Ensure the input value is valid and parse it as an integer.
            if (inputValue is not null && int.TryParse(inputValue.ToString(), out int newQuantity))
            {
                // Update the Quantity field in the model.
                item.Quantity = newQuantity;

                // Notify the EditContext about the field change and trigger validation.
                NotifyFieldChanged(item, nameof(item.Quantity));
            }
        }





        public AppState EditQuantity(int productID, int qty)
        {

            feedbackMessage = "";

            try
            {
                var productInShoppingLists = shoppingCart.Where(x => x.ProductID == productID).FirstOrDefault();

                if (productInShoppingLists == null)
                {
                    throw new ArgumentNullException("there is no item with the productID in the shopping list item.");
                }


                //let's not hit the server yet.
                //var productInDatabase = stocks.Where(x => x.StockItemID == productID).FirstOrDefault();

                //if (productInDatabase == null)
                //{
                //    throw new ArgumentException("There is no such item in the record.");
                //}

                if (qty <= 0)
                {
                    productInShoppingLists.Quantity = 1;

                    throw new ArgumentException("Quantity should be greater than 0. Modified to 1 as default.");
                }

                //let's not hit the server yet.
                //if (qty > productInDatabase.QuantityOnHand)
                //{
                //    productInShoppingLists.Quantity = productInDatabase.QuantityOnHand;

                //    throw new ArgumentException($"You can't order more than the quantity on hand. Modified to maximum: {productInDatabase.QuantityOnHand}.");
                //}

                productInShoppingLists.Quantity = qty;
                AppState.cartView = shoppingCart;
                feedbackMessage = $"Quantity edited to {productInShoppingLists.Quantity}.";


            }
            catch (AggregateException ex)
            {
                foreach (var error in ex.InnerExceptions)
                {
                    feedbackMessage += error.Message + Environment.NewLine;
                }
            }
            catch (ArgumentNullException ex)
            {
                feedbackMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }
            catch (Exception ex)
            {
                feedbackMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }
            //StateHasChanged();
            return AppState;
        }




        public AppState RemoveItem(int productID)
        {
            if (productID == null)
            {
                throw new ArgumentNullException("Product ID should be supplied.");
            }

            if (productID <= 0)
            {
                throw new ArgumentNullException("Invalid ProductID for removing.");
            }

            var productInShoppingLists = AppState.cartView.Where(x => x.ProductID == productID).FirstOrDefault();

            if (productInShoppingLists == null)
            {
                throw new ArgumentNullException($"No item with the productID:{productID} in the shopping list");
            }
            else
            {
                AppState.cartView.Remove(productInShoppingLists);
                shoppingCart = AppState.cartView;   //they need to match always for UI
                feedbackMessage = $"The Product {productInShoppingLists.ProductName} has been removed.";

                //shoppingcart variable is automatically tracking Appstate. So, without assigning value again here, UI is also updated.. surprised.
            }


            return AppState;
        }


    }
}
