using RMSSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMSSystem.ViewModels;
using Microsoft.EntityFrameworkCore;
using RMSSystem.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RMSSystem.BLL
{
    public class RentalService
    {

        private readonly RMSContext _rentalContext;



        internal RentalService(RMSContext rentalContext)
        {
            //Initialize the _hogWildContext field with the provided HogWildContext instance
            _rentalContext = rentalContext;
        }



        //public List<RentalSearchView> GetRentalOneInput(string city, string community)
        //{
        //    //Business Rules
        //    //these are processing rules that need to be satisfied
        //    //for valid data

        //    //rule: both last name and phone number cannot be empty (둘다 비어있으면안됨)
        //    //Rule: RemoveFromViewFlag must be false



        //    //Need to update parameters so we are not searching on an empty value.
        //    //Otherwise, an empty string will return all records
        //    if (string.IsNullOrWhiteSpace(city))
        //    {
        //        city = Guid.NewGuid().ToString();
        //    }
        //    if (string.IsNullOrWhiteSpace(community))
        //    {
        //        community = Guid.NewGuid().ToString();
        //    }

        //    //Return Customers that meet our criteria

        //    return _rentalContext.Rentals
        //                    //.Include(x => x.RentalType)
        //                    .Where(x => (x.Address.City.Contains(city.Trim()) || x.Address.Community.Contains(community.Trim()))
        //                                && !x.RemoveFromViewFlag)
        //                    .Select(x => new RentalSearchView
        //                    {
        //                        RentalID = x.RentalID,
        //                        City = x.Address.City,
        //                        Community = x.Address.Community,
        //                        RentalType = x.RentalType.Description,
        //                        MaxVacancy = x.MaxVacancy,
        //                        AvailableVacancy = x.MaxVacancy - x.Renters.Count(),
        //                        MonthlyRent = x.MonthlyRent,
        //                        DamageDeposit = x.DamageDeposit

        //                    }).OrderBy(x => x.RentalID)
        //                    .ToList();

        //}



        public List<RentalSearchView> GetRentalsWithOneInput(string city, string community)
        {
            //Business Rules
            //these are processing rules that need to be satisfied
            //for valid data

            //rule: both last name and phone number cannot be empty (둘다 비어있으면안됨)
            //Rule: RemoveFromViewFlag must be false



            //Need to update parameters so we are not searching on an empty value.
            //Otherwise, an empty string will return all records
            if (string.IsNullOrWhiteSpace(city))
            {
                city = Guid.NewGuid().ToString();
            }
            if (string.IsNullOrWhiteSpace(community))
            {
                community = Guid.NewGuid().ToString();
            }

            //Return Customers that meet our criteria

            return _rentalContext.Rentals
                            //.Include(x => x.RentalType)
                            .Where(x => (x.Address.City.Contains(city.Trim()) || x.Address.Community.Contains(community.Trim()))
                                        && !x.RemoveFromViewFlag)
                            .Select(x => new RentalSearchView
                            {
                                RentalID = x.RentalID,
                                City = x.Address.City,
                                Community = x.Address.Community,
                                RentalType = x.RentalType.Description,
                                MaxVacancy = x.MaxVacancy,
                                AvailableVacancy = x.MaxVacancy - x.Renters.Where(a => a.RemoveFromViewFlag == false).Count(),
                                MonthlyRent = x.MonthlyRent,
                                DamageDeposit = x.DamageDeposit

                            })
                            .OrderBy(x => x.City)
                            .ThenBy(x => x.Community)
                            .ToList();

        }

        public List<RentalSearchView> GetRentalsWithAllInput(string city, string community)
        {
            //Business Rules
            //these are processing rules that need to be satisfied
            //for valid data

            //rule: both last name and phone number cannot be empty (둘다 비어있으면안됨)
            //Rule: RemoveFromViewFlag must be false

            //Need to update parameters so we are not searching on an empty value.
            //Otherwise, an empty string will return all records

            //Return Customers that meet our criteria

            return _rentalContext.Rentals
                            //.Include(x => x.RentalType)
                            .Where(x => (x.Address.City.Contains(city.Trim()) && x.Address.Community.Contains(community.Trim()))
                                        && !x.RemoveFromViewFlag)
                            .Select(x => new RentalSearchView
                            {
                                RentalID = x.RentalID,
                                City = x.Address.City,
                                Community = x.Address.Community,
                                RentalType = x.RentalType.Description,
                                MaxVacancy = x.MaxVacancy,
                                AvailableVacancy = x.MaxVacancy - x.Renters.Where(a => a.RemoveFromViewFlag == false).Count(),
                                MonthlyRent = x.MonthlyRent,
                                DamageDeposit = x.DamageDeposit

                            })
                            .OrderBy(x => x.City)
                            .ThenBy(x => x.Community)
                            .ToList();

        }


        public RentalView GetRental(int rentalID)
        {
            if (rentalID < 0)
            {
                throw new ArgumentNullException("Invalid Rental ID.");
            }

            var rentalIndatabase = _rentalContext.Rentals.Where(x => x.RentalID == rentalID).FirstOrDefault();
            if (rentalIndatabase == null)
            {
                throw new ArgumentNullException("Database doesn't have this record.");
            }

            return _rentalContext.Rentals
                                        .Where(x => x.RentalID == rentalID && x.RemoveFromViewFlag == false)
                                        .Select(x => new RentalView
                                        {
                                            RentalID = x.RentalID,
                                            AddressID = x.AddressID,
                                            RentalTypeID = x.RentalTypeID,
                                            MonthlyRent = x.MonthlyRent,
                                            DamageDeposit = x.DamageDeposit,
                                            MaxVacancy = x.MaxVacancy,
                                            Vacancies = x.MaxVacancy - x.Renters.Where(a => a.RemoveFromViewFlag == false).Count(),
                                            AvailableDate = x.AvailableDate,
                                            Address = new AddressView
                                            {
                                                Unit = x.Address.Unit,
                                                Number = x.Address.Number,
                                                Street = x.Address.Street,
                                                Community = x.Address.Community,
                                                City = x.Address.City,
                                                LandLord = x.Address.LandLord.Name
                                            },
                                            Renters = x.Renters.Where(a => a.Active == true)
                                                                .Select(a => new RenterView
                                                                {
                                                                    RenterID = a.RenterID,
                                                                    StudentNumber = a.StudentNumber,
                                                                    StudentName = a.StudentNumberNavigation.FirstName + " " + a.StudentNumberNavigation.LastName,
                                                                    EmergencyContactName = a.EmergencyContactName,
                                                                    EmergencyContactNumber = a.EmergencyContactNumber,
                                                                    RemoveFromViewFlag = a.RemoveFromViewFlag

                                                                }).ToList()
                                        }).FirstOrDefault();


        }

        public List<RentalTypeView> GetRentalTypeList()
        {
            return _rentalContext.RentalTypes.Where(x => x.RemoveFromViewFlag == false)
                                            .Select(x => new RentalTypeView
                                            {
                                                RentalTypeID = x.RentalTypeID,
                                                Description = x.Description

                                            })
                                            .OrderBy(x => x.Description)
                                            .ToList();
        }




        public int AddEditRental(RentalView currentRental)
        {
            List<Exception> errorDetails = new List<Exception>();

            if (currentRental == null)
                throw new ArgumentNullException("You must supply Rental information.");


            if (currentRental.MonthlyRent <= 0)
            {
                errorDetails.Add(new Exception("Monthly Rent must be greater than zero."));
            }

            if (currentRental.DamageDeposit <= 0)
            {
                errorDetails.Add(new Exception("Damage Deposit must be greater than zero."));
            }

            if (currentRental.DamageDeposit > currentRental.MonthlyRent)
            {
                errorDetails.Add(new Exception("Damage Deposit cannot exceed Monthly Rent."));
            }

            if (currentRental.MaxVacancy <= 0)
            {
                errorDetails.Add(new Exception("Max Vacancy must be greater than zero."));
            }

            if (currentRental.AvailableDate == null)
            {
                errorDetails.Add(new Exception("AvailableDate cannot be null."));
            }

            List<RenterView> notFalseRenters = currentRental.Renters.Where(x => x.RemoveFromViewFlag == false).ToList();
            if (notFalseRenters.Count == 0)
            {
                errorDetails.Add(new Exception("There should be at least one renter."));
            }

            if (currentRental.RentalTypeID == 0)
            {
                errorDetails.Add(new Exception("RentalType should be selected."));
            }

            if (notFalseRenters.Count > currentRental.MaxVacancy)
            {
                errorDetails.Add(new Exception("Total Renters cannot be more than Max Vacancy."));
            }


            int errorCount = 0;
            foreach (var renter in currentRental.Renters)
            {

                if (string.IsNullOrWhiteSpace(renter.EmergencyContactName))
                {
                    errorCount++;
                }
                if (string.IsNullOrWhiteSpace(renter.EmergencyContactNumber))
                {
                    errorCount++;
                }

            }

            if (errorCount > 0)
            {
                errorDetails.Add(new Exception("Emergency Contact Name and Number should be provided for all renters."));
            }


            Rental rental = _rentalContext.Rentals.Where(x => x.RentalID == currentRental.RentalID && x.RemoveFromViewFlag == false).FirstOrDefault();

            if (rental == null)
            {
                rental = new Rental();
                rental.Renters = new List<Renter>();
                rental.RentalPayments = (ICollection<RentalPayment>)new List<RentalPayment>();
            }

            rental.RentalPayments = rental.RentalPayments.Count == 0 ? (ICollection<RentalPayment>)new List<RentalPayment>() : rental.RentalPayments;


            //messenger
            //Address address = new Address();
            //RentalType rentalType = new();
            //List<RentalPayment> rentalPayment = new();


            //assign
            rental.AddressID = currentRental.AddressID;
            rental.RentalTypeID = currentRental.RentalTypeID;
            rental.MonthlyRent = currentRental.MonthlyRent;
            rental.MaxVacancy = currentRental.MaxVacancy;
            rental.DamageDeposit = currentRental.DamageDeposit;
            rental.AvailableDate = currentRental.AvailableDate;
            rental.RemoveFromViewFlag = currentRental.RemoveFromViewFlag;


            //I'm not sure if this is also working. //since I'm not changing any data below regarding address,
            //just loading from db would be a better way I guess..?

            //address.AddressID = item.Address.AddressID;
            //address.Number = item.Address.Number;
            //address.Street = item.Address.Street;
            //address.City = item.Address.City;
            //address.Community = item.Address.Community;
            //address.Unit = item.Address.Unit;
            //address.ProvinceState = item.Address.ProvinceState;
            //address.PostalCodeZip = item.Address.PostalCodeZip;
            //address.CountryCode = item.Address.CountryCode;
            //address.LandLordID = item.Address.LandLordID;
            //address.RemoveFromViewFlag = item.Address.RemoveFromViewFlag;
            //rental.Address = address;


            //just loading. //this is not needed? By assigning address ID, EF automatically follows and fills this section???? as a foreign key ?
            //rental.Address = _rentalContext.Addresses.Where(x => x.AddressID == currentRental.Address.AddressID).FirstOrDefault();


            var matchingRentalType = _rentalContext.RentalTypes.Where(x => x.RentalTypeID == currentRental.RentalTypeID && x.RemoveFromViewFlag == false)
                                                                .Select(x => x.Description).FirstOrDefault();



            //in case the database has been edited and rentaltype list has been changed.
            if (matchingRentalType == null && currentRental.RentalTypeID!=0)
            {
                errorDetails.Add(new Exception($"Check if your database has the rentayltypeID : {currentRental.RentalTypeID}"));
            }


            //else
            //{
            //    currentRental.Description = matchingRentalType;
            //}
            //rentalType.RemoveFromViewFlag = 

            //rental.RentalType = rentalType;


            //rental.RentalPayments = (ICollection<RentalPayment>)rentalPayment;


            foreach (var eachRenterView in currentRental.Renters)
            {
                Renter renter = _rentalContext.Renters.Where(x => x.RenterID == eachRenterView.RenterID).FirstOrDefault();
                if (renter == null)
                {
                    renter = new Renter();
                    renter.StudentNumber = eachRenterView.StudentNumber;

                    //I don't have any info on what this effective date is. need to ask.
                    ////renter.EffectiveDate = eachRenterView.EffectiveDate;

                    ///I assume newly added renters or updated renters are 'active' but I need to ask James about this.
                    //renter.Active = true;
                }


                //I need to know how to set these two values.
                //if I use this, effectivedate is going to be the same..               
                //renter.EffectiveDate = renter.EffectiveDate;
                renter.EffectiveDate = DateTime.Now;


                //renter.Active = renter.Active;
                if (eachRenterView.RemoveFromViewFlag == false)
                {
                    renter.Active = true;
                }
                else
                {
                    renter.Active = false;
                }


                //other than these two, when I update or add data, I need to consider all 'related' tables, which should added or updated accordingly.
                //otherwise my db data would look ugly.
                renter.EmergencyContactNumber = eachRenterView.EmergencyContactNumber;
                renter.EmergencyContactName = eachRenterView.EmergencyContactName;
                renter.RemoveFromViewFlag = eachRenterView.RemoveFromViewFlag;

                //I once thought about using the code below, but I found that through EF core, all related tables and data are updated accordingly.
                //     var student = _rentalContext.Students
                //.Where(s => s.StudentNumber == renter.StudentNumber)
                //.FirstOrDefault();
                ////renter.StudentNumberNavigation = student;

                if (renter.RenterID == 0)
                {
                    rental.Renters.Add(renter);

                }
                else
                {
                    _rentalContext.Renters.Update(renter);
                }
            }


            if (errorDetails.Count > 0)
            {
                _rentalContext.ChangeTracker.Clear();
                throw new AggregateException("Failed to Save.", errorDetails);
            }

            if (currentRental.RentalID == 0)
            {
                _rentalContext.Rentals.Add(rental);
            }
            else
            {
                _rentalContext.Rentals.Update(rental);
            }

            _rentalContext.SaveChanges();


            //if I use return item.RentalID, it will always return 0.(this is addign a new record) and item is just a viewmodel.
            //I'm not using Entity object(which will reflect newly assigned id) like I did in CPSC1517. 
            return rental.RentalID;

        }
    }
}
