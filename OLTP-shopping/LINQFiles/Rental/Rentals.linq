<Query Kind="Program">
  <Connection>
    <ID>3a81508c-4bf9-479f-b702-45574fb81b15</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Server>.</Server>
    <Database>eTools2023</Database>
    <DisplayName>eTools2023-Entity</DisplayName>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>




void Main()
{
	//  placeholder for pass or fail
	string passFail = string.Empty;

	#region Get Customers (GetCustomerByContactPhone)
	//  Header information
	Console.WriteLine("=========================================");
	Console.WriteLine("           Get CustomerByContactPhone    ");
	Console.WriteLine("=========================================");
	Console.WriteLine();

	//  Pass
	Console.WriteLine("----- Test: Valid Contact Phone -----");
	var customers = TestGetCustomerByContactPhone("780.444.4444").Dump();
	passFail = customers.Count() == 2 ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - Expected 2 customers, found {customers.Count()}");

	Console.WriteLine("----- Test: Invalid Contact Phone -----");
	customers = TestGetCustomerByContactPhone("780.433.9999");
	passFail = customers.Count() == 0 ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - No customer found for '780.433.9999'");

	//  Fail
	Console.WriteLine();
	Console.WriteLine("----- Test: Missing Contact Phone -----");
	customers = TestGetCustomerByContactPhone(string.Empty);
	passFail = customers == null ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - Empty customer name should return null");
	
	Console.WriteLine();
	Console.WriteLine("===================================");
	#endregion

	#region Get Products (GetEquipments)
	//  Header information
	Console.WriteLine("===================================");
	Console.WriteLine("           Get Equipments          ");
	Console.WriteLine("===================================");
	Console.WriteLine();

	//  Pass	
	Console.WriteLine("----- Test: Valid Equipments available -----");
	var products = TestGetEquipments().Dump();
	passFail = products.Count() >= 0 ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - Expected 0 or more products, found {products.Count()}");
	
	Console.WriteLine();
	Console.WriteLine("===================================");
	#endregion
	
	#region Get Rental By Rental Number (GetRentalByRentalNumber)
	//  Header information
	Console.WriteLine("====================================================");
	Console.WriteLine("           Get Get Rental By Rental Number          ");
	Console.WriteLine("====================================================");
	Console.WriteLine();

	//  Pass	
	Console.WriteLine("----- Test: Valid Rental Number -----");
	var rental = TestGetRentalByRentalNumber(1).Dump();
	passFail = rental != null ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - Expected 1 rental found 0");

	Console.WriteLine("----- Test: Missing Rental Number -----");
	rental = TestGetRentalByRentalNumber(9999).Dump();
	passFail = rental == null ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - Expected 0 rental found 1");

	//  Fail
	Console.WriteLine("----- Test: Invalid Rental Number -----");
	rental = TestGetRentalByRentalNumber(0).Dump();
	passFail = rental == null ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - Rental number 0 should return null");
	
	Console.WriteLine();
	Console.WriteLine("===================================");
	#endregion
	
	#region Get Rental By CustomerPhone (GetRentalByPhone)
	//  Header information
	Console.WriteLine("====================================================");
	Console.WriteLine("           Get Get Rental By Customer Phone         ");
	Console.WriteLine("====================================================");
	Console.WriteLine();

	//  Pass	
	Console.WriteLine("----- Test: Valid Customer Contact Phone -----");
	var rentals = TestGetRentalByPhone("780.128.7170").Dump();
	passFail = rentals.Count > 0 ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - Expected 1 or more rentals");

	Console.WriteLine("----- Test: Missing Customer Contact Phone -----");
	rentals = TestGetRentalByPhone("780.128.9999").Dump();
	passFail = rentals.Count == 0 ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - Expected 0 rental found 1 or more");

	//  Fail
	Console.WriteLine("----- Test: Invalid Customer Contact Phone -----");
	rentals = TestGetRentalByPhone(string.Empty).Dump();
	passFail = rentals == null ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - Empty customer contact number should return null");
	
	Console.WriteLine();
	Console.WriteLine("===================================");
	#endregion
	
	#region Get Rentals By Customer (GetRentalsByCustomer)
	//  Header information
	Console.WriteLine("===============================================");
	Console.WriteLine("           Get Get Rentals By Customer          ");
	Console.WriteLine("===============================================");
	Console.WriteLine();

	//  Pass	
	Console.WriteLine("----- Test: Valid CustomerID -----");
	rentals = TestSelectRentalsByCustomer(28).Dump();
	passFail = rentals.Count > 0 ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - Expected 1 or more rentals");

	Console.WriteLine("----- Test: Missing CustomerID -----");
	rentals = TestSelectRentalsByCustomer(9999).Dump();
	passFail = rentals.Count == 0 ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - Expected 0 rental found 1 or more");

	Console.WriteLine("----- Test: Invalid CustomerID -----");
	rentals = TestSelectRentalsByCustomer(0).Dump();
	passFail = rentals == null ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - Customer number 0 should return null");
	
	Console.WriteLine();
	Console.WriteLine("===================================");
	#endregion
	
#region Get Couon (GetCoupon)
	//  Header information
	Console.WriteLine("========================");
	Console.WriteLine("           Get Coupon   ");
	Console.WriteLine("========================");
	Console.WriteLine();

	//  Pass
	Console.WriteLine("----- Test: Valid CouponID -----");
	var couponDiscount = TestGetCoupon(4).Dump();
	passFail = couponDiscount == 5 ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - Expected coupon discount == 5, received {couponDiscount}");

	//  Fail
	Console.WriteLine();
	Console.WriteLine("----- Test: Unexisting CouponID -----");
	couponDiscount = TestGetCoupon(1).Dump();
	passFail = couponDiscount == null ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - Expected coupon discount == null, received coupon discount value is null: {couponDiscount == null}");

	Console.WriteLine();
	Console.WriteLine("----- Test: Invalid CouponID -----");
	couponDiscount = TestGetCoupon(0).Dump();
	passFail = couponDiscount == null ? "Pass" : "Fail";
	Console.WriteLine($"{passFail} - Expected coupon discount == null, received coupon discount value is null: {couponDiscount == null}");
	
	Console.WriteLine();
	Console.WriteLine("===================================");
	#endregion	
	
	Rent();
	
	Return(couponID: 4, rentalDateIn: DateTime.Now.AddDays(2), paymentType: "M", comments: "none", condition: "damaged");
	
}

public void Rent()
{
	var rentedEquipment = RentalEquipments.Where(x => x.Available && !x.Retired && !x.RemoveFromViewFlag).Take(2)
	.Where(x => x.Available && !x.RemoveFromViewFlag && !x.Retired).ToList();
	
	var numberOfDays = 0.5;
	
	var newRental = new Rentals()
	{
		CustomerID = 4,
		EmployeeID = 5,
		CouponID = 4,
		RentalDateOut = DateTime.Now,
		RentalDateIn = DateTime.Now.AddDays(numberOfDays),
		PaymentType = "C"
	};
	
	foreach(RentalEquipment equipment in rentedEquipment)
	{
		equipment.Available = false;
		
		var rentalDetails = new RentalDetails()
		{
			RentalEquipmentID = equipment.RentalEquipmentID,
			RentalDays = (decimal)numberOfDays,
			RentalRate = equipment.DailyRate,
			OutCondition = equipment.Condition,
			InCondition  = equipment.Condition
		};
		
		newRental.RentalDetails.Add(rentalDetails);
	}
	
	Rentals.InsertOnSubmit(newRental);
	
	SubmitChanges();
}

public void Return(int couponID, DateTime rentalDateIn, string paymentType, string comments, string condition)
{
	var customerRental = Rentals.OrderByDescending(r => r.RentalID).First();
	
	var coupon = Coupons.First(x => x.CouponID == couponID);
	
	customerRental.CouponID = coupon.CouponID;
	customerRental.RentalDateIn = rentalDateIn > customerRental.RentalDateIn ? rentalDateIn : customerRental.RentalDateIn;
	customerRental.PaymentType = paymentType;
	
	foreach(RentalDetails rentalDetail in customerRental.RentalDetails)
	{
		rentalDetail.RentalDays = (decimal)((customerRental.RentalDateIn - customerRental.RentalDateOut).TotalDays );
		
		if(rentalDetail.RentalDays < (decimal)0.5)
			rentalDetail.RentalDays = (decimal)0.5;
			
		customerRental.SubTotal = customerRental.SubTotal + (rentalDetail.RentalDays * rentalDetail.RentalRate);
		rentalDetail.InCondition = condition;
		rentalDetail.DamageRepairCost = 0;
		rentalDetail.Comments = comments;
		
		var equipment = RentalEquipments.First(x => x.RentalEquipmentID == rentalDetail.RentalEquipmentID);
		
		equipment.Available = true;		
		equipment.Condition = rentalDetail.InCondition;
		
		if(equipment.Condition == "damaged")
		{
			equipment.Available = false;
			equipment.Retired = true;
		}
	}
	
	customerRental.TaxAmount = customerRental.SubTotal * (decimal)0.05;
	
	SubmitChanges();
}

//#region Support Methods
public Exception GetInnerException(System.Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}


//	This region contains methods used for testing the functionality
//	of the application's business logic and ensuring correctness.
//#region Test Methods
//
public int? TestGetCoupon(int couponID) {
		try 
		{
			return GetCoupon(couponID);
		}
		catch (AggregateException ex)
		{
			foreach (var error in ex.InnerExceptions)
			{
				error.Message.Dump();
			}
		}
		catch (ArgumentNullException ex)
		{
			GetInnerException(ex).Message.Dump();
		}

		catch (Exception ex)
		{
			GetInnerException(ex).Message.Dump();
		}
		
		return null;
	}	
	
	public List<CustomerView> TestGetCustomerByContactPhone(string contactPhone) {
		try 
		{
			return GetCustomerByContactPhone(contactPhone);
		}
		catch (AggregateException ex)
		{
			foreach (var error in ex.InnerExceptions)
			{
				error.Message.Dump();
			}
		}
		catch (ArgumentNullException ex)
		{
			GetInnerException(ex).Message.Dump();
		}

		catch (Exception ex)
		{
			GetInnerException(ex).Message.Dump();
		}
		return null;
	}	
	
	public List<AvailableEquipmentView> TestGetEquipments()
	{
	    try
	    {
	        return GetEquipments();
	    }
		catch (AggregateException ex)
		{
			foreach (var error in ex.InnerExceptions)
			{
				error.Message.Dump();
			}
		}
		catch (ArgumentNullException ex)
		{
			GetInnerException(ex).Message.Dump();
		}

		catch (Exception ex)
		{
			GetInnerException(ex).Message.Dump();
		}
	    return null;
	}

	public RentalsView TestGetRentalByRentalNumber(int rentalNumber)
	{
	    try
	    {
	        return GetRentalByRentalNumber(rentalNumber);
	    }
		catch (AggregateException ex)
		{
			foreach (var error in ex.InnerExceptions)
			{
				error.Message.Dump();
			}
		}
		catch (ArgumentNullException ex)
		{
			GetInnerException(ex).Message.Dump();
		}

		catch (Exception ex)
		{
			GetInnerException(ex).Message.Dump();
		}
	    return null;
	}

	public List<RentalsView> TestGetRentalByPhone(string customerContactPhone)
	{
	    try
	    {
	        return GetRentalByPhone(customerContactPhone);
	    }
		catch (AggregateException ex)
		{
			foreach (var error in ex.InnerExceptions)
			{
				error.Message.Dump();
			}
		}
		catch (ArgumentNullException ex)
		{
			GetInnerException(ex).Message.Dump();
		}

		catch (Exception ex)
		{
			GetInnerException(ex).Message.Dump();
		}
	    return null;
	}

	public List<RentalsView> TestSelectRentalsByCustomer(int customerID)
	{
	    try
	    {
	        return SelectRentalsByCustomer(customerID);
	    }
		catch (AggregateException ex)
		{
			foreach (var error in ex.InnerExceptions)
			{
				error.Message.Dump();
			}
		}
		catch (ArgumentNullException ex)
		{
			GetInnerException(ex).Message.Dump();
		}

		catch (Exception ex)
		{
			GetInnerException(ex).Message.Dump();
		}
	    return null;
	}

//#endregion
	
#region Methods

public int GetCoupon(int couponID)
{
	if(couponID == 0)
	{
		throw new Exception("CouponID cannot be 0");
	}

	return Coupons.FirstOrDefault(c => c.CouponID == couponID).CouponDiscount;
}

 public List<CustomerView> GetCustomerByContactPhone(string customerContactPhone) 
 {
 	if(customerContactPhone == "") 
	{
		throw new  ArgumentNullException("This fields cannot be empty");
	}
	
	return Customers
		.Where(x => x.RemoveFromViewFlag == false &&
			x.ContactPhone.ToLower().Contains(customerContactPhone.ToLower()))
		.Select(x => new CustomerView{
			CustomerID = x.CustomerID,
			LastName = x.LastName,
			FirstName = x.FirstName, 
			Address = x.Address,
			City = x.City,
			ProvinceDescription = x.Province.Description
		})
		.ToList();
 }
 
 public List<AvailableEquipmentView> GetEquipments()
{    
    return RentalEquipments
        .Where(x => x.RemoveFromViewFlag == false &&
					x.Available &&
                    x.Retired == false)
        .Select(x => new AvailableEquipmentView
        {
            RentalEquipmentID = x.RentalEquipmentID,
			RentalEquipmentDescription = x.Description,
			RentalEquipmentSerialNumber = x.SerialNumber,
			RentalEquipmentDailyRate = x.DailyRate
        })
        .ToList();
}

 
 public RentalsView GetRentalByRentalNumber(int rentalID)
{    
	if(rentalID is not > 0)
	{
		throw new  ArgumentNullException("This invalid rentalID");
	}
	
    return Rentals
        .Where(x => x.RemoveFromViewFlag == false &&
					x.RentalID == rentalID)
        .Select(x => new RentalsView
        {
            RentalID = x.RentalID,
            CustomerID = x.CustomerID,
            EmployeeFirstName = x.Employee.FirstName,
            EmployeeLastName = x.Employee.LastName,
            CustomerFirstName = x.Customer.FirstName,
            CustomerLastName = x.Customer.LastName,
			CustomerAddress = x.Customer.Address,
			CustomerCity = x.Customer.City,
			RentalDate = x.RentalDateOut,
			DaysOut = DateTime.Now.Subtract(x.RentalDateOut).Days,
			PaymentType = x.PaymentType,
			RentalDetailsList = x.RentalDetails.Select(y => new RentalDetailView
			{
				RentalDetailID = y.RentalDetailID,
				RentalEquipmentDescription = y.RentalEquipment.Description,
				RentalEquipmentSerialNumber = y.RentalEquipment.SerialNumber,
				RentalEquipmentDailyRate = y.RentalEquipment.DailyRate,
				RentalEquipmentCondition = y.OutCondition,
				RentalDetailComment = y.Comments,
				DamageCharge = y.DamageRepairCost
			}).ToList()
        })
        .FirstOrDefault();
}

 
 public List<RentalsView> GetRentalByPhone(string customerContactPhone)
{    
 	if(customerContactPhone == "") 
	{
		throw new  ArgumentNullException("This fields cannot be empty");
	}
	
    return Rentals
        .Where(x => x.RemoveFromViewFlag == false &&
					x.Customer.ContactPhone == customerContactPhone)
        .Select(x => new RentalsView
        {
            RentalID = x.RentalID,
            CustomerID = x.CustomerID,
            EmployeeFirstName = x.Employee.FirstName,
            EmployeeLastName = x.Employee.LastName,
            CustomerFirstName = x.Customer.FirstName,
            CustomerLastName = x.Customer.LastName,
			CustomerAddress = x.Customer.Address,
			CustomerCity = x.Customer.City,
			RentalDate = x.RentalDateOut,
			DaysOut = DateTime.Now.Subtract(x.RentalDateOut).Days,
			PaymentType = x.PaymentType,
			RentalDetailsList = x.RentalDetails.Select(y => new RentalDetailView
			{
				RentalDetailID = y.RentalDetailID,
				RentalEquipmentDescription = y.RentalEquipment.Description,
				RentalEquipmentSerialNumber = y.RentalEquipment.SerialNumber,
				RentalEquipmentDailyRate = y.RentalEquipment.DailyRate,
				RentalEquipmentCondition = y.OutCondition,
				RentalDetailComment = y.Comments,
				DamageCharge = y.DamageRepairCost
			}).ToList()
        })
        .ToList();
}
 
 public List<RentalsView> SelectRentalsByCustomer(int customerID)
{    
	if(customerID is not > 0)
	{
		throw new  ArgumentNullException("This invalid customerID");
	}
	
    return Rentals
        .Where(x => x.RemoveFromViewFlag == false &&
					x.Customer.CustomerID == customerID)
        .Select(x => new RentalsView
        {
            RentalID = x.RentalID,
            CustomerID = x.CustomerID,
            EmployeeFirstName = x.Employee.FirstName,
            EmployeeLastName = x.Employee.LastName,
            CustomerFirstName = x.Customer.FirstName,
            CustomerLastName = x.Customer.LastName,
			CustomerAddress = x.Customer.Address,
			CustomerCity = x.Customer.City,
			RentalDate = x.RentalDateOut,
			DaysOut = DateTime.Now.Subtract(x.RentalDateOut).Days,
			PaymentType = x.PaymentType,
			RentalDetailsList = x.RentalDetails.Select(y => new RentalDetailView
			{
				RentalDetailID = y.RentalDetailID,
				RentalEquipmentDescription = y.RentalEquipment.Description,
				RentalEquipmentSerialNumber = y.RentalEquipment.SerialNumber,
				RentalEquipmentDailyRate = y.RentalEquipment.DailyRate,
				RentalEquipmentCondition = y.OutCondition,
				RentalDetailComment = y.Comments,
				DamageCharge = y.DamageRepairCost
			}).ToList()
        })
        .ToList();
}

#endregion

//	This region includes the view models used to 
//	represent and structure data for the UI.
#region View Models
	
	public class CustomerView 
	{
		public int CustomerID {get; set;}
		public string LastName {get; set;}
		public string FirstName {get; set;}
		public string Address {get; set;}	
		public string City {get; set;}	
		public string ProvinceDescription {get; set;}
	}
	
	public class RentalsView
	{
		public int RentalID {get; set;} 
		public int CustomerID {get; set;}
		public string EmployeeFirstName {get; set;}
		public string EmployeeLastName {get; set;}
		public string CustomerFirstName {get; set;}
		public string CustomerLastName {get; set;}
		public string CustomerAddress {get; set;}
		public string CustomerCity {get; set;}
		public DateTime RentalDate {get; set;}
		public int DaysOut {get; set;}
		public string PaymentType {get; set;}
		public List<RentalDetailView> RentalDetailsList {get; set;}
	}	
	
	public class RentalDetailView 
	{
		public int RentalDetailID {get; set;}
		public string RentalEquipmentDescription {get; set;}
		public string RentalEquipmentSerialNumber {get; set;}
		public decimal RentalEquipmentDailyRate {get; set;}
		public string RentalEquipmentCondition {get; set;}
		public string RentalDetailComment {get; set;}
		public decimal DamageCharge {get; set;}
		public bool Available {get; set;}
	}
	
	public class AvailableEquipmentView {
		public int RentalEquipmentID {get; set;}
		public string RentalEquipmentDescription {get; set;}
		public string RentalEquipmentSerialNumber {get; set;}
		public decimal RentalEquipmentDailyRate {get; set;}
	}

#endregion
