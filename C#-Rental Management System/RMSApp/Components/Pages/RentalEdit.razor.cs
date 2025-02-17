using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Identity.Client;
using MudBlazor;
using RMSSystem.BLL;
using RMSSystem.ViewModels;
using System.Drawing;
using System.Net;



namespace RMSApp.Components.Pages
{
    public partial class RentalEdit
    {

        [Parameter] public int RentalID { get; set; }



    


        [Inject]
        protected AddressService AddressService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected RentalService RentalService { get; set; }

        [Inject]
        protected StudentService StudentService { get; set; }

        [Inject]
        protected RenterService RenterService { get; set; }

        [Inject]
        protected IDialogService DialogService { get; set; }
        private TaskCompletionSource<bool?> dialogCompletionSource;
        private string dialogMessage = "";
        private bool showDialog = false;


        private string closeButtonText = "Close";
        private MudBlazor.Color closeButtonColor = MudBlazor.Color.Success;
        private EditContext editContext;

        //private bool disableSaveButton => !editContext.IsModified() || !editContext.Validate();
        private ValidationMessageStore messageStore;





        private AddressView AddressView { get; set; }
        private RenterView currentRenter { get; set; }

        public string lastName { get; set; }

        public List<StudentView> Students { get; set; } = new();

        private string feedbackMessage = "";

        private string errorMessage = "";

        private bool hasFeedback => !string.IsNullOrWhiteSpace(feedbackMessage);

        private bool hasError => !string.IsNullOrWhiteSpace(errorMessage);
        private List<string> errorDetails = new();
        int SavedRentalID = 0;

        private RentalView currentRental { get; set; } = new();

        private List<RentalTypeView> rentalTypeList { get; set; } = new();

        //private int vacancies => currentRental.MaxVacancy - currentRental.Renters.Count() + numOfRemoved;
        private int vacancies => currentRental.MaxVacancy - currentRental.Renters.Where(x => x.RemoveFromViewFlag == false).Count();

        //numOfRemoved is needed for calculating vacancies.
        //Current RentersList sometimes has a student whose removedfromViewFlag turns false instead of being deleted.
        //This student doesn't pop out of the list, so he still has his place, which makes the calculation a little difficult.
        //so instead of using actual 'count' of the List, I added numOfRemoved to get the current available number(considering removedfromViewFlag student).
        private int numOfRemoved = 0;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            editContext.Validate();
        }

        protected override async void OnInitialized()
        {

         
            try
            {
                rentalTypeList = RentalService.GetRentalTypeList();

                if (RentalID != 0)
                {
                    SavedRentalID = RentalID;
                    currentRental = RentalService.GetRental(RentalID);
                }
                else
                {
                   
                    AddressView = AddressService.GetNextUnusedAddress();
                    currentRental.AddressID = AddressView.AddressID; //without this..i experienced a lot of error..:(
                    currentRental.Address = new AddressView
                    {
                        AddressID = AddressView.AddressID,
                        Number = AddressView.Number,
                        Street = AddressView.Street,
                        City = AddressView.City,
                        Community = AddressView.Community,
                        Unit = AddressView.Unit,
                        ProvinceState = AddressView.ProvinceState,
                        PostalCodeZip = AddressView.PostalCodeZip,
                        CountryCode = AddressView.CountryCode,
                        LandLordID = AddressView.LandLordID,
                        RemoveFromViewFlag = AddressView.RemoveFromViewFlag
                    };
                }

                editContext = new EditContext(currentRental);

                editContext.OnValidationRequested += HandleValidationRequested;
                messageStore = new ValidationMessageStore(editContext);
                editContext.OnFieldChanged += EditContext_OnFieldChanged;
                errorDetails.Clear();
                errorMessage = "";
                feedbackMessage = "";

                //what does this code do?? invokeasync??
                await InvokeAsync(StateHasChanged);
                base.OnInitialized();
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
                errorMessage = $"{errorMessage} Failed to Save.";
                foreach (var error in ex.InnerExceptions)
                {
                    errorDetails.Add(error.Message);
                }
            }
        }


        private void GetStudents()
        {
            feedbackMessage = "";
            errorMessage = "";
            try
            {
                Students = StudentService.getStudents(lastName, currentRental.Renters);
                if (Students.Count == 0)
                {
                    errorMessage = "Can't find a student with that lastname who is not a renter for this property.";
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
                errorMessage = $"{errorMessage}Unable to search for Student";
                foreach (var error in ex.InnerExceptions)
                {
                    errorDetails.Add(error.Message);
                }
            }
        }


        private void AddToRenters(int studentNumber, int rentalID)
        {


            try
            {
                currentRenter = RenterService.getRenter(studentNumber, rentalID);

                if (vacancies <= 0)
                {
                    throw new ArgumentException("Can't add more. It's full.");
                }
                currentRental.Renters.Add(currentRenter);
                StudentView matchingStudent = Students.FirstOrDefault(a => a.StudentNumber == studentNumber);


                if (matchingStudent != null)
                {
                    Students.Remove(matchingStudent);
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
                errorMessage = $"{errorMessage}Unable to Add Renters.";
                foreach (var error in ex.InnerExceptions)
                {
                    errorDetails.Add(error.Message);
                }
            }
        }

        private void RemoveFromList(int studentNumber)
        {

            List<RenterView> listOfRenters = currentRental.Renters.ToList();
            RenterView removeStudent = listOfRenters.Where(a => a.StudentNumber == studentNumber).FirstOrDefault();
            StudentView matchingStudent = StudentService.getStudentView(studentNumber);

            if (removeStudent != null && matchingStudent != null && removeStudent.RenterID == 0)
            {
                currentRental.Renters.Remove(removeStudent);
                Students.Add(matchingStudent);
                errorMessage = "";
            }

            if (removeStudent != null && matchingStudent != null && removeStudent.RenterID != 0)
            {
                RenterView existingRenter = currentRental.Renters.Where(a => a.StudentNumber == studentNumber).FirstOrDefault();
                existingRenter.RemoveFromViewFlag = true;
                //Students.Add(matchingStudent);
                numOfRemoved++;
                errorMessage = "";
            }
        }

        private void Restore(int studentNumber)
        {
            RenterView existingRenter = currentRental.Renters.Where(a => a.StudentNumber == studentNumber).FirstOrDefault();
            existingRenter.RemoveFromViewFlag = false;
            numOfRemoved--;
        }


        private void Save()
        {
            errorDetails.Clear();
            errorMessage = "";
            feedbackMessage = "";

            try
            {
                SavedRentalID = RentalService.AddEditRental(currentRental);
                if (currentRental.RentalID == 0)
                {
                    feedbackMessage = $"Success - Your New Rental ID is {SavedRentalID}.";
                    StateHasChanged();
                }
                else
                {
                    feedbackMessage = $"Success - Rental ID {SavedRentalID} has been updated.";
                    StateHasChanged();
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
                errorMessage = $"{errorMessage}Unable to Save Rental.";
                foreach (var error in ex.InnerExceptions)
                {
                    errorDetails.Add(error.Message);
                }
            }
        }


        private async Task Cancel()
        {
            dialogMessage = "Do you wish to close the rental editor?";
            bool? result = await ShowDialogAsync();

            if (result == true)
            {
                // If "Ok" is clicked
                NavigationManager.NavigateTo("/RentalList");
            }

            // If "Cancel
            // fallout
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


        private void EditContext_OnFieldChanged(object sender, FieldChangedEventArgs e)
        {
            editContext.Validate();
            closeButtonText = editContext.IsModified() ? "Cancel" : "Close";
            closeButtonColor = editContext.IsModified() ? MudBlazor.Color.Warning : MudBlazor.Color.Default;
        }


        private void HandleValidationRequested(object sender, ValidationRequestedEventArgs e)
        {
            messageStore?.Clear();

            if (currentRental == null)
                throw new ArgumentNullException("You must supply Rental information.");



            if (currentRental.MonthlyRent <= 0)
            {
                messageStore?.Add(()=>currentRental.MonthlyRent, "Monthly Rent must be greater than zero.");
            }

            if (currentRental.DamageDeposit <= 0)
            {
                messageStore?.Add(() => currentRental.DamageDeposit, "Damage Deposit must be greater than zero.");
            }

            if (currentRental.DamageDeposit > currentRental.MonthlyRent)
            {
                messageStore?.Add(() => currentRental.DamageDeposit, "Damage Deposit cannot exceed Monthly Rent.");
            }

            if (currentRental.MaxVacancy <= 0)
            {
                messageStore?.Add(() => currentRental.MaxVacancy, "Max Vacancy must be greater than zero.");
            }

            if (currentRental.AvailableDate == null)
            {
                messageStore?.Add(() => currentRental.AvailableDate, "AvailableDate cannot be null.");
            }

            foreach (var renter in currentRental.Renters)
            {
                if (string.IsNullOrWhiteSpace(renter.EmergencyContactName))
                {
                    messageStore?.Add(() => renter.EmergencyContactName, "Emergency Contact Name is required.");
                }
                if (string.IsNullOrWhiteSpace(renter.EmergencyContactNumber))
                {
                    messageStore?.Add(() => renter.EmergencyContactNumber, "Emergency Contact Number is required.");
                }
            }


            if (currentRental.RentalTypeID == 0)
            {
                messageStore?.Add(() => currentRental.RentalTypeID, "RentalType should be selected.");
            }

            //if (notFalseRenters.Count > currentRental.MaxVacancy)
            //{
            //    errorDetails.Add(new Exception("Total Renters cannot be more than Max Vacancy."));
            //}


            ////int errorCount = 0;
            ////foreach (var renter in currentRental.Renters)
            ////{

            ////    if (string.IsNullOrWhiteSpace(renter.EmergencyContactName))
            ////    {
            ////        errorCount++;
            ////    }
            ////    if (string.IsNullOrWhiteSpace(renter.EmergencyContactNumber))
            ////    {
            ////        errorCount++;
            ////    }

            ////}

            //if (errorCount > 0)
            //{
            //    errorDetails.Add(new Exception("Emergency Contact Name and Number should be provided for all renters."));
            //}


        }

     





    }


}







