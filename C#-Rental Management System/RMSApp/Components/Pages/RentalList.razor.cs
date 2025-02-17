using RMSSystem.BLL;
using RMSSystem.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace RMSApp.Components.Pages
{
    public partial class RentalList
    {
        private string cityName="";

        private string commuName="";


        private string feedbackMessage;

        //the error message
        private string errorMessage;


        private bool hasFeedback => !string.IsNullOrWhiteSpace(feedbackMessage);

        //has error
        private bool hasError => !string.IsNullOrWhiteSpace(errorMessage);

        private List<string> errorDetails = new();


        protected List<RentalSearchView> Rentals { get; set; } = new();

        [Inject]
        protected RentalService RentalService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        //Gets or sets the pagination with an initial value of 10 times per page.
        protected PaginationState Pagination = new PaginationState { ItemsPerPage = 10 };



        private void Search()
        {
            try
            {
                errorDetails.Clear();
                errorMessage = string.Empty;
                feedbackMessage = string.Empty;

                Rentals.Clear();

                if (string.IsNullOrWhiteSpace(cityName) && string.IsNullOrWhiteSpace(commuName))
                {
                    throw new ArgumentException("Please provide either a last name and/or phone number");
                }

                //one input
                if ((string.IsNullOrWhiteSpace(cityName) && !string.IsNullOrWhiteSpace(commuName)) || (!string.IsNullOrWhiteSpace(cityName) && string.IsNullOrWhiteSpace(commuName)))
                {
                    Rentals = RentalService.GetRentalsWithOneInput(cityName, commuName);
                }

                //two inputs
                if (!(string.IsNullOrWhiteSpace(cityName) && string.IsNullOrWhiteSpace(commuName)))
                {
                    Rentals = RentalService.GetRentalsWithAllInput(cityName, commuName);
                }



                if (Rentals.Count > 0)
                {
                    feedbackMessage = "Search for Rental(s) was successful";
                }
                else
                {
                    feedbackMessage = "No Rental was found for your search criteria";
                }

            }
            catch (ArgumentNullException ex)
            {
                errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }
            catch (ArgumentException ex)
            {
                errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }
            catch (AggregateException ex)
            {
                //  have a collection of errors
                //  each error should be place into a separate line
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    errorMessage = $"{errorMessage}{Environment.NewLine}";
                }
                errorMessage = $"{errorMessage}Unable to search for rental";
                foreach (var error in ex.InnerExceptions)
                {
                    errorDetails.Add(error.Message);
                }
            }
        }

        public int placeholder = 0;
        private void New()
        {
            NavigationManager.NavigateTo("/RentalEdit/0");
        }

        private void EditRental(int rentalID)
        {
            NavigationManager.NavigateTo($"/RentalEdit/{rentalID}");
        }

    }
}
