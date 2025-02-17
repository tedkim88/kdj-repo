<Query Kind="Program">
  <Connection>
    <ID>a54ac80b-b13d-452c-8ec1-71407f8b75f8</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Server>.</Server>
    <Database>eTools2023</Database>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

// Driver is responsible for orchestrating the flow by calling
// various methods and classes that contain the actual business logic
// or data processing operations.
void Main()
{
	//GetCategories (completed)
	//GetPartsByCategory (completed)
	//GetCoupons (completed)
	//SaveSale (completed)
	//GetSaleReturn (completed)
	//SaveSaleRefund (completed)


	//(Required) Methods
	#region GetCategories
	//GetCategories
	TestGetCategories().Dump("Success-Get All Categories.");
	Console.WriteLine("==============================");
	#endregion

	#region GetItemsByCategoryID
	//(Required)GetItemsByCategoryID
	TestGetItemsByCategoryID(-3).Dump("Fail. Invalid(negative) categoryid");
	Console.WriteLine("==============================");
	TestGetItemsByCategoryID(0).Dump("Fail. Invalid(zero) categoryid");
	Console.WriteLine("==============================");
	TestGetItemsByCategoryID(4).Dump("Success. GetItems of categoryID 4");
	Console.WriteLine("==============================");
	#endregion

	#region TestAddToShoppingList
	//TestAddToShoppingLists
	TestAddToShoppingLists(34, 1).Dump("Success - (dependent on database QOH) Add the first item to empty list");
	Console.WriteLine("==============================");
	TestAddToShoppingLists(34, 2).Dump("Success(dependent on database QOH) - Duplicate item exist. quantity added to the existing item");
	Console.WriteLine("==============================");
	TestAddToShoppingLists(82, 4).Dump("Success -Add another item");
	Console.WriteLine("==============================");
	TestAddToShoppingLists(83, 3).Dump("Success -Add another item");
	Console.WriteLine("==============================");
	TestAddToShoppingLists(84, 5).Dump("Success -Add another item");
	Console.WriteLine("==============================");
	TestAddToShoppingLists(5580, 1).Dump("Success -Add another item");
	Console.WriteLine("==============================");
	TestAddToShoppingLists(5580, 1).Dump("Success -Duplicate Item Quantity added to the existing item");
	Console.WriteLine("==============================");
	TestAddToShoppingLists(5580, 1).Dump("Fail. You can't order more than the quantity on hand"); //quantity on hand for id5580 is 2 in the database.
	Console.WriteLine("==============================");
	TestAddToShoppingLists(5555, 6).Dump("Success -Add another item with qty of 6"); //quantity on hand for id5580 is 2 in the database.
	Console.WriteLine("==============================");
	TestAddToShoppingLists(5555, 0).Dump("Fail. Qty input should be greater than 0");
	Console.WriteLine("==============================");
	TestAddToShoppingLists(24, 1).Dump("Fail. No such product in the database");
	Console.WriteLine("==============================");
	#endregion

	#region TestEditQuantity of shoppingList
	//TestEditQuantity of shoppingLists
	TestEditQuantity(5580, 0).Dump("Fail.Quantity should be greater than 0");
	Console.WriteLine("==============================");
	TestEditQuantity(5580, 10).Dump("Fail.Quantity for Edit cannot be over Quantity On Hand");
	Console.WriteLine("==============================");
	TestEditQuantity(5580, 1).Dump("Successfully adjust(edit) Quantity to 1");
	Console.WriteLine("==============================");
	#endregion

	#region TestRemovingItem
	//Test for Removing Item from the cart
	TestRemovingItem(0).Dump("Fail. ProductID can't be zero");
	Console.WriteLine("==============================");
	TestRemovingItem(-3).Dump("Fail. No negative number for productID");
	Console.WriteLine("==============================");
	TestRemovingItem(5580).Dump("Success -The result of removing : no 5580 item anymore");
	Console.WriteLine("==============================");


	shoppingListItems.Dump("Success - The Current Shopping ListItem");
	Console.WriteLine("==============================");
	#endregion

	#region TestGetCoupon
	//GetCoupon
	Console.WriteLine("======GetCouponRate(coupon validation)====================");
	TestGetCouponRate("Joy23").Dump("Fail. Joy23 is invalid due to expiration.");
	Console.WriteLine("==============================");
	
	int? successCouponRate = TestGetCouponRate("Good10").Dump("Success - Coupon Validation. Good10 is Valid.");
	Console.WriteLine($"The coupon rate is {successCouponRate}");
	Console.WriteLine("==============================");
	#endregion


	#region TestSaveSales
	//(Required)SaveSales
	Console.WriteLine("====================TEST ADD SALES=====================");
	var failSaleID = TestAddSales(shoppingListItems, 1, "Joy23", "D", 0).Dump("Fail. The Joy23 coupon has expired. Try another Coupon."); //new record saleID is always 0. // 
	Console.WriteLine("==============================");
	var SuccessSaleID = TestAddSales(shoppingListItems, 1, "Good10", "D", 0).Dump("Success with Coupon(Good10) provided--discount rate 10 percent."); //new record saleID is always 0. // 
	Console.WriteLine($"{SuccessSaleID} is new sale ID.");
	Console.WriteLine("==============================");
	#endregion

	#region TestGetSaleRefund
	//(Required)GetSaleRefund
	TestGetSaleRefund(null).Dump("Fail. GetSaleRefund : SaleID cannot be null");
	Console.WriteLine("==============================");
	TestGetSaleRefund(0).Dump("Fail. 0 is Invalid saleID. SaleID should be greater than 0");
	Console.WriteLine("==============================");
	TestGetSaleRefund(1).Dump("Success. GetSale of saleId 1");
	Console.WriteLine("==============================");
	TestGetSaleRefund(Sales.Count()).Dump("Success - GetSaleRefund with the most recent saleID"); //check with the most recent one
	Console.WriteLine("==============================");
	#endregion

	#region Test Edit Quantity for return
	//Edit quantity for return
	TestEditReturnQuantity(0, 5555).Dump("Fail. Invalid number. Return quantity should be greater than 0.");
	Console.WriteLine("==============================");
	TestEditReturnQuantity(10, 5555).Dump("Fail. Invalid request. can't return more than bought.");
	Console.WriteLine("==============================");
	TestEditReturnQuantity(3, 5555).Dump("Success-return quantity edited");
	Console.WriteLine("==============================");
	TestEditReturnQuantity(2, 5555).Dump("Success-return quantity edited");
	Console.WriteLine("==============================");
	TestEditReturnQuantity(5, 5555).Dump("Success-return quantity edited");
	Console.WriteLine("==============================");
	TestEditReturnQuantity(2, 82).Dump("Success-quantity edited");
	Console.WriteLine("==============================");
	TestEditReturnQuantity(20, 5555).Dump("Fail. you can't return more than you bought");
	Console.WriteLine("==============================");
	#endregion

	#region Test AddSaleRefund
	//(Required)and SaveRefund //fired when clicking 'refund'button from UI
	TestAddSaleRefund(returnList, 7).Dump("Fail. This employee doesn't have a proper position to conduct sale and refund transaction.");
	Console.WriteLine("==============================");
	var saleRefundID = TestAddSaleRefund(returnList, 1).Dump("Successfully adding refund.");
	Console.WriteLine($"saleRefundID is {saleRefundID}");
	Console.WriteLine("==============================");
	#endregion

	//I'm assuming that, in our project scenario, refund is possible only one time with the SAME sale ID.
	//Because returnList data is based on Getsale(from sale table). 
	//But once a customer returns some items, GetSale Data doesn't actually reflect the changed quantity that the customer now has (Available Quantity for return has changed).
	//if a person has purchased a certain item of qty 6, and returns 3, the remaining in customer's hand is actually 3 now.
	//but for the second refund later, he visits again in a week.
	// Then if I execute getsale method with the same saleID(now twice) --which is from sales table
	// the get sale info would show he still has 6items. Because Saledata doesn't change even after return, and GetSale method gets the unchanged data for the second refund.
}

// This region contains methods used for testing the functionality
// of the application's business logic and ensuring correctness.
#region Test Methods


public List<CategoryView> TestGetCategories()
{
	try
	{
		return GetCategories();
	}

	#region catch all exceptions (define later)

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

	#endregion
	return null; //Ensures a valid return value even on failure

}



public List<StockItemView> TestGetItemsByCategoryID(int categoryID)
{
	try
	{
		return GetItemsByCategoryID(categoryID);
	}

	#region catch all exceptions (define later)

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

	#endregion
	return null; //Ensures a valid return value even on failure

}

public List<ShoppingItemsView> TestAddToShoppingLists(int productID, int qty)
{
	try
	{
		return AddToShoppingLists(productID, qty);
	}

	#region catch all exceptions (define later)

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

	#endregion
	return null; //Ensures a valid return value even on failure

}



public int? TestGetCouponRate(string couponValue)
{
	try
	{
		return GetCouponRate(couponValue);
	}

	#region catch all exceptions (define later)

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

	#endregion
	return null; //Ensures a valid return value even on failure
}


public int? TestAddSales(List<ShoppingItemsView> shoppingListItems, int employeeID, string couponIDValue, string paymentType, int saleID)
{
	try
	{
		return AddSales(shoppingListItems, employeeID, couponIDValue, paymentType, saleID);
	}

	#region catch all exceptions (define later)

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

	#endregion
	return null; //Ensures a valid return value even on failure

}





public List<ShoppingItemsView> TestEditQuantity(int productID, int qty)
{
	try
	{
		return EditQuantity(productID, qty);
	}

	#region catch all exceptions (define later)

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

	#endregion
	return null; //Ensures a valid return value even on failure

}





public List<ShoppingItemsView> TestRemovingItem(int productID)
{
	try
	{
		return RemovingItem(productID);
	}

	#region catch all exceptions (define later)

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

	#endregion
	return null; //Ensures a valid return value even on failure

}




public List<ReturnItemView> TestGetSaleRefund(int? saleID)
{
	try
	{
		return GetSaleRefund(saleID);
	}

	#region catch all exceptions (define later)

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

	#endregion
	return null; //Ensures a valid return value even on failure

}




public List<ReturnItemView> TestEditReturnQuantity(int qty, int stockItemID)
{
	try
	{
		return EditReturnQuantity(qty, stockItemID);
	}

	#region catch all exceptions (define later)

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

	#endregion
	return null; //Ensures a valid return value even on failure

}

public int? TestAddSaleRefund(List<ReturnItemView> returnList, int employeeID)
{
	try
	{
		return AddSaleRefund(returnList, employeeID);
	}

	#region catch all exceptions (define later)

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

	#endregion
	return null; //Ensures a valid return value even on failure

}



#endregion

// This region contains support methods for testing
#region Support Methods
public Exception GetInnerException(System.Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}
#endregion

// This region contains all methods responsible
// for executing business logic and operations.


#region Methods

//get all categories
public List<CategoryView> GetCategories()
{
	return Categories
					.Where(x => x.RemoveFromViewFlag == false)
					.Select(x => new CategoryView
					{
						CategoryID = x.CategoryID, //maybe not needed to show user in blazor
						Description = x.Description,
						NumberOfItems = x.StockItems.Where(a => a.Discontinued == false && a.RemoveFromViewFlag == false)
													.Count()

					}).ToList();


}

//GetItemsByCategoryID
public List<StockItemView> GetItemsByCategoryID(int categoryID)
{

	#region Business Logic and Parameter Exceptions
	//create a list<Exception> to contain all discovered errors
	List<Exception> errorList = new List<Exception>();

	//Business Rules
	//These are processing rules that need to be satisfied
	// for valid data

	// rule : categoryID should be valid(greater than 0)
	if (categoryID <= 0)
	{
		throw new ArgumentException("Category ID is invalid(should be greater than 0)");
	}


	var matchingCategory = Categories
									.Where(x => x.CategoryID == categoryID && x.RemoveFromViewFlag == false)
									.FirstOrDefault();

	// checking if there is matching category in DB.
	if (matchingCategory == null)
	{
		throw new ArgumentException("There is no matching category in the database.");
	}

	#endregion

	return StockItems
					.Where(x => x.RemoveFromViewFlag == false &&
								x.Discontinued == false &&
								x.CategoryID == categoryID)
					.Select(x => new StockItemView
					{
						StockItemID = x.StockItemID,
						Description = x.Description,
						SellingPrice = x.SellingPrice

					}).ToList();

}


public List<ShoppingItemsView> AddToShoppingLists(int productID, int qty)
{

	//create a list<Exception> to contain all discovered errors
	List<Exception> errorList = new List<Exception>();

	// productID should be greater than 0
	if (productID <= 0)
	{
		errorList.Add(new Exception("Product ID should be greater than 0"));
	}

	//Quantity for adding should be at least 1.
	if (qty <= 0)
	{
		throw new ArgumentException("Quantity should be greater than 0");
	}

	var productInDatabase = StockItems.Where(x => x.StockItemID == productID &&
												x.RemoveFromViewFlag == false &&
												x.Discontinued == false).FirstOrDefault();

	//Checking if there is matching Item in the DB.
	if (productInDatabase == null)
	{
		throw new ArgumentNullException("There is no such item in the database.");
	}

	//Checking quantity on hand.
	if (qty > productInDatabase.QuantityOnHand)
	{
		throw new ArgumentException($"You can't order more than quantity on hand. Quantity on hand is {productInDatabase.QuantityOnHand}");
	}

	//Checking if there is any quantity.
	if (productInDatabase.QuantityOnHand <= 0)
	{
		errorList.Add(new Exception("We don't have quantity on hand for this item now."));
	}


	//checking if the item to be added is already in the shopping list.
	var existingItem = shoppingListItems.Where(x => x.ProductID == productID).FirstOrDefault();
	if (existingItem != null)
	{
		//quantity checking against DB.
		if (existingItem.Quantity + qty > productInDatabase.QuantityOnHand)
		{
			errorList.Add(new Exception("You can't order more than the quantity on hand."));
		}
		else
		{
			//adding quantity to the existing item.
			Console.WriteLine("Duplicate item is already in the cart! quantity added to the existing Item");
			existingItem.Quantity += qty;
		}
	}
	else //there is no matching item in the shoppinglist
	{
		//adding a new list to the existing shoppinglist variable
		shoppingListItems.Add(new ShoppingItemsView
		{
			ProductID = productID,
			ProductName = productInDatabase.Description,
			Quantity = qty,
			UnitPrice = productInDatabase.SellingPrice,
		});
	}
	if (errorList.Count > 0)
	{
		//Clearing the "track changes" ensures consistency in our entity system.
		//Otherwise we leave our entity system in flux
		ChangeTracker.Clear();

		//test

		//Throw an AggregateException containing the list of business processing errors.
		throw new AggregateException("Failed to add to shopping list. Please check erroor message(s)", errorList);
	}
	else
	{
		return shoppingListItems;
	}
}




public List<ShoppingItemsView> EditQuantity(int productID, int qty)
{

	//create a list<Exception> to contain all discovered errors
	List<Exception> errorList = new List<Exception>();

	//you can't change qty to 0. if you want this, you should just remove this item.
	if (qty <= 0)
	{
		errorList.Add(new Exception("quantity should be greater than 0"));
	}

	//checking matching product in db
	var productInDatabase = StockItems.Where(x => x.StockItemID == productID &&
												x.Discontinued == false &&
												x.RemoveFromViewFlag == false).FirstOrDefault();

	if (productInDatabase == null)
	{
		throw new ArgumentException("There is no such item in the record.");
	}

	//need to find the item that we are going to work on for edit.
	var productInShoppingLists = shoppingListItems.Where(x => x.ProductID == productID).FirstOrDefault();

	//you can't edit when there is no item you are looking for in the list.
	if (productInShoppingLists == null)
	{
		throw new ArgumentNullException("there is no item with the productID in the shopping list item.");
	}

	//QOH should be considered when editing
	if (qty > productInDatabase.QuantityOnHand)
	{
		errorList.Add(new Exception($"You can't order more than the quantity on hand. Quantity on hand : {productInDatabase.QuantityOnHand}."));
	}
	else
	{
		productInShoppingLists.Quantity = qty;
	}
	if (errorList.Count > 0)
	{
		//Clearing the "track changes" ensures consistency in our entity system.
		//Otherwise we leave our entity system in flux
		ChangeTracker.Clear();
		//Throw an AggregateException containing the list of business processing errors.
		throw new AggregateException("Failed to change quantity. Please check erroor message(s)", errorList);
	}
	else
	{
		return shoppingListItems;
	}
}




public List<ShoppingItemsView> RemovingItem(int productID)
{

	//create a list<Exception> to contain all discovered errors
	List<Exception> errorList = new List<Exception>();

	//product id should be greater than 0
	if (productID <= 0)
	{
		errorList.Add(new Exception("Invalid ProductID for removing."));
	}

	//checking product in list
	var productInShoppingLists = shoppingListItems.Where(x => x.ProductID == productID).FirstOrDefault();

	if (productInShoppingLists == null)
	{
		throw new ArgumentNullException($"No item with the productID:{productID} in the shopping list");
	}
	else
	{
		shoppingListItems.Remove(productInShoppingLists);
	}

	if (errorList.Count > 0)
	{
		//Clearing the "track changes" ensures consistency in our entity system.
		//Otherwise we leave our entity system in flux
		ChangeTracker.Clear();
		//Throw an AggregateException containing the list of business processing errors.
		throw new AggregateException("Failed to remove item. Please check erroor message(s)", errorList);
	}
	else
	{
		return shoppingListItems;
	}
}


public int GetCouponRate(string couponValue)
{
	List<Exception> errorList = new List<Exception>();

	if (string.IsNullOrWhiteSpace(couponValue))
	{
		throw new ArgumentException("Please enter your coupon for validation.(case-sensitive)");
	}

	var matchCoupon = Coupons.Where(x => x.CouponIDValue == couponValue && x.RemoveFromViewFlag == false).FirstOrDefault();
	var currentTime = DateTime.Now;


	if (matchCoupon == null)
	{
		errorList.Add(new Exception("there is no matching Coupon"));
	}
	else
	{
		if (currentTime < matchCoupon.StartDate || currentTime > matchCoupon.EndDate)
		{
			errorList.Add(new Exception($"{matchCoupon.CouponIDValue} has expired. Please provide another coupon."));
		}
	}

	if (errorList.Count > 0)
	{
		//Clearing the "track changes" ensures consistency in our entity system.
		//Otherwise we leave our entity system in flux
		ChangeTracker.Clear();
		//Throw an AggregateException containing the list of business processing errors.
		throw new AggregateException("Failed to Get Coupon. Please check erroor message(s)", errorList);
	}
	else
	{
		return Coupons.Where(x => x.CouponIDValue == couponValue && x.RemoveFromViewFlag == false)
								   .Select(x => x.CouponDiscount).FirstOrDefault();
	}


}


public int AddSales(List<ShoppingItemsView> shoppingListItems, int employeeID, string couponIDValue, string paymentType, int saleID)
{
	#region Business Logic and Parameter Exceptions
	//    create a list<Exception> to contain all discovered errors
	List<Exception> errorList = new List<Exception>();
	//  Business Rules
	//    These are processing rules that need to be satisfied
	//        for valid data
	//    rule:    shoppingItemView cannot be null
	if (shoppingListItems == null)
	{
		throw new ArgumentNullException("No shopping list was supplied");
	}

	//when adding sales, the list should have at least one item
	if (shoppingListItems.Count == 0)
	{
		errorList.Add(new Exception("There should be at least one shopping item in the list."));
	}

	//checking employeeID validity
	if (employeeID <= 0)
	{
		errorList.Add(new Exception("Invalid Employee ID! should be greater than 0."));
	}


	var checkPosition = Employees
								.Where(x => x.EmployeeID == employeeID && x.RemoveFromViewFlag == false)
								.FirstOrDefault();

	//there is no matching Employee in DB
	if (checkPosition == null) //checking matching employee
	{
		throw new ArgumentException($"There is no matching employee with ID {employeeID}.");
	}

	//checking positions. only certain roles can do this transaction.
	if (!(checkPosition.Position.Description == "Store Manager" || checkPosition.Position.Description == "Associate"))
	{
		errorList.Add(new Exception("This Employee is not allowed to do sales and returns transaction."));
	}

	int discount = 0; //this is discount percentage, default to 0.

	//checking coupon
	var matchingCoupon = Coupons
								.Where(x => x.CouponIDValue == couponIDValue && x.RemoveFromViewFlag == false)
								.FirstOrDefault();

	//boolean variable to check later whether coupon has been expired.
	bool CouponExpired = false;

	if (!string.IsNullOrWhiteSpace(couponIDValue)) //only check coupon when it is supplied.
	{
		if (matchingCoupon == null)
		{
			throw new ArgumentException($"There is no matching coupon of {couponIDValue}.");
		}

		//checking coupon expiration date
		//the time of adding new sales is now
		var currentTime = DateTime.Now;

		if (currentTime < matchingCoupon.StartDate || currentTime > matchingCoupon.EndDate)
		{
			CouponExpired = true;
			errorList.Add(new Exception($"{matchingCoupon.CouponIDValue} has expired. Please provide another coupon."));

		}
		else //if coupon is valid and has not expired
		{
			discount = matchingCoupon.CouponDiscount; //this is discount percentage
		}
	}

	var upperPaymentType = paymentType.ToUpper(); //to make sure that this parameter is case-insensitive
	if (!(upperPaymentType == "M" || upperPaymentType == "C" || upperPaymentType == "D"))
	{
		errorList.Add(new Exception("PaymentType should be M or C or D."));
	}

	#endregion


	Sales sale = Sales
					.Where(x => x.SaleID == saleID)
					.FirstOrDefault();


	//since we are not editing after sales--it is always adding a new record according to the rules.
	//sale object above should always return null when made(no matching saleID). (sale id 0)
	if (sale != null) //is there is matching data
	{
		throw new ArgumentNullException("You can't change already existing sale Record.");
	}


	sale = new Sales();
	sale.SaleDate = DateTime.Now;
	sale.SaleDetails = new();
	sale.EmployeeID = employeeID;
	sale.PaymentType = upperPaymentType;
	sale.CouponID = matchingCoupon == null ? null
					: CouponExpired == true ? null : matchingCoupon.CouponID;

	sale.SubTotal = shoppingListItems.Sum(a => a.Subtotal); //this is before applying discount for saving database
	sale.TaxAmount = sale.SubTotal * 0.05m; //this is before applying discount for saving database

	//when saving data into database, I decided not to apply any discount.
	//but I do calculate discount for the customer and show discounted price and tax to the customer for payment.
	//in return transaction as well.



	//decimal Total = sale.SubTotal * (100 - discount) / 100 + sale.TaxAmount; //this is for UI
	//talked with James about the way of calculating tax(before applying discount VS after applying discount)
	//and was told to go with 'after discount' for tax calculation.
	//so this 'before discount' approach for tax calculation is gone.

	decimal Total = sale.SubTotal * (100 - discount) / 100 + sale.TaxAmount * (100 - discount) / 100; //get Total for customer to pay(discount applied if applicable).




	//adding item lists to saleDetails.
	foreach (var item in shoppingListItems)
	{
		SaleDetails saleDetails = new();
		saleDetails.StockItemID = item.ProductID;
		saleDetails.SellingPrice = item.UnitPrice;
		saleDetails.Quantity = item.Quantity;
		sale.SaleDetails.Add(saleDetails);
	}

	Sales.Add(sale);

	//from here update stock item //to reduce item quantities from stock after sale
	foreach (var item in shoppingListItems)
	{
		var matchingStockItem = StockItems.Where(x => x.StockItemID == item.ProductID &&
													x.RemoveFromViewFlag == false &&
													x.Discontinued == false)
													.FirstOrDefault();
		if (matchingStockItem == null)
		{
			throw new ArgumentNullException("There is no matching Stock Item.");
		}

		if (item.Quantity > matchingStockItem.QuantityOnHand)
		{
			throw new ArgumentException($"You can't buy more than quantity on hand. Current Quantity for item {item.ProductName} is {matchingStockItem.QuantityOnHand}");
		}

		matchingStockItem.QuantityOnHand -= item.Quantity;
		StockItems.Update(matchingStockItem);
	}

	if (errorList.Count > 0)
	{
		//Clearing the "track changes" ensures consistency in our entity system.
		//Otherwise we leave our entity system in flux
		ChangeTracker.Clear();
		//Throw an AggregateException containing the list of business processing errors.
		throw new AggregateException("Failed to Add Sales. Please check error message(s)", errorList);
	}
	else
	{
		Console.WriteLine($"{Total} is total price including tax with discount applied(if applicable)"); //just for checking in linqpad
		SaveChanges();
		return sale.SaleID;
	}

}



public List<ReturnItemView> GetSaleRefund(int? saleID)
{
	#region Business Logic and Parameter Exceptions
	//create a list<Exception> to contain all discovered errors
	List<Exception> errorList = new List<Exception>();

	//Business Rules
	//These are processing rules that need to be satisfied
	// for valid data
	// rule : saleID should be valid(greater than 0)

	isCouponUsed = false; //for resetting the value for getting new saleID
	matchingCouponRate = 0; //for resetting the value for getting new saleID

	//checking validity of saleid
	if (saleID == null || saleID <= 0)
	{
		throw new ArgumentException("Invalid SaleID. Valid SaleID is required for returning.");
	}

	var matchingSaleDetail = SaleDetails
									.Where(x => x.SaleID == saleID && x.RemoveFromViewFlag == false)
									.FirstOrDefault();

	//check if the customer has actually purchased something.
	if (matchingSaleDetail == null)
	{
		throw new ArgumentException("There is no matching sale Detail with the saleID.");
	}


	var matchingSale = Sales.Where(x => x.SaleID == saleID && x.RemoveFromViewFlag == false).FirstOrDefault();
	//checking matchign sale
	if (matchingSale == null)
	{
		throw new ArgumentNullException("There is no matching Sale.");
	}

	if (matchingSale != null)
	{
		if (matchingSale.CouponID != null)
		{
			//checking if a coupon was applied to the sale.
			isCouponUsed = true;
		}
	}

	Console.WriteLine($"Coupon used?(T/F):{isCouponUsed}. for this saleID{saleID}.");

	#endregion

	var matchingSaleDetails = SaleDetails
									.Where(x => x.SaleID == saleID &&
												x.RemoveFromViewFlag == false)
									.ToList();

	//let's use this variable if coupon has been applied to each item with a certain discount rate.
	matchingCouponRate = Sales.Where(x => x.SaleID == saleID && x.CouponID != null && x.RemoveFromViewFlag == false)
								.Select(x => x.Coupon.CouponDiscount)
								.FirstOrDefault();

	Console.WriteLine($"{matchingCouponRate} is matching CouponRate Percentage");

	//assigning to this variable for later(when we need to edit return quantity)
	returnList = matchingSaleDetails
						.Select(x => new ReturnItemView
						{
							StockItemID = x.StockItemID,
							Item = x.StockItem.Description,
							SaleID = x.SaleID,
							AvailQTY = x.Quantity,
							OriginalPrice = x.SellingPrice * x.Quantity, //this is total price paid for each item.
							SellingUnitPrice = x.SellingPrice,
							RtnQty = 0,
							discountFlag = isCouponUsed,

						}).ToList();

//this is gonna be used later for editreturnQty,serving as the initial value, which shouldn'tchange.
	cloneforComparison = returnList
	.Select(item => new ReturnItemView
	{
		StockItemID = item.StockItemID,
		Item = item.Item,
		SaleID = item.SaleID,
		AvailQTY = item.AvailQTY,
		OriginalPrice = item.OriginalPrice,
		SellingUnitPrice = item.SellingUnitPrice,
		RtnQty = item.RtnQty,
		discountFlag = item.discountFlag,
	}).ToList();

	return returnList;
}


//Every time I click Change qty button for updating refund info, I'll call this method.
public List<ReturnItemView> EditReturnQuantity(int qty, int stockItemID)
{
	//create a list<Exception> to contain all discovered errors
	List<Exception> errorList = new List<Exception>();

	//selecting item which is to be edited
	var matchingItem = returnList
								.Where(x => x.StockItemID == stockItemID)
								.FirstOrDefault();

	var matchingCloneItem = cloneforComparison.Where(x => x.StockItemID == stockItemID)
								.FirstOrDefault();

	if (matchingItem == null)
	{
		throw new ArgumentNullException("there is no matching Item with that name.");
	}

	//can't change to 0
	if (qty < 0)
	{
		throw new ArgumentException("Quantity can't be a negative number.");
	}

	//can't return more than bought
	if (qty > matchingCloneItem.AvailQTY)
	{
		throw new ArgumentException("You can't return more than you have bought for this item.");
	}


	if (errorList.Count > 0)
	{
		//Clearing the "track changes" ensures consistency in our entity system.
		//Otherwise we leave our entity system in flux
		ChangeTracker.Clear();
		//Throw an AggregateException containing the list of business processing errors.
		throw new AggregateException("Failed to edit return quantity. Please check erroor message(s)", errorList);
	}

	//Users may continuously change return quantity. It may increase, or decrease. Availqty should also change accordingly.

	//when increasing rtn qty from the previous data
	if (qty > matchingItem.RtnQty)
	{
		var difference = qty - matchingItem.RtnQty;
		matchingItem.RtnQty = qty;
		matchingItem.AvailQTY -= difference;
	}

	//when decreasing rtn qty from the previous data
	if (qty < matchingItem.RtnQty)
	{
		var difference = matchingItem.RtnQty - qty;
		matchingItem.RtnQty = qty;
		matchingItem.AvailQTY += difference;
	}

	//the title is original price(according to the sample). But it is the subtotal of the specific item that customer still has in his hand.
	matchingItem.OriginalPrice = matchingItem.AvailQTY * matchingItem.SellingUnitPrice;


	subTotalOfReturnItems = returnList.Sum(a => a.RtnQty * a.SellingUnitPrice);

	taxOfReturnItems = subTotalOfReturnItems * 0.05m;
	//this is tax calculation before discount is applied.
	//I'll include discount for tax as well in 'totalReturn' for customer.
	//but in database just decided not to reflect discount. talked about this with James.


	totalReturn = subTotalOfReturnItems * (100 - matchingCouponRate) / 100 + taxOfReturnItems * (100 - matchingCouponRate) / 100;
	//totalReturn is for customer and it is applying discount to tax as well. 

	//checking the result in Linqpad.



	Console.WriteLine($"Your total return is {totalReturn} including tax.");
	return returnList;

}




public int AddSaleRefund(List<ReturnItemView> returnList, int employeeID)
{
	#region Business Logic and Parameter Exceptions
	//    create a list<Exception> to contain all discovered errors
	List<Exception> errorList = new List<Exception>();
	//  Business Rules
	//    These are processing rules that need to be satisfied
	//        for valid data


	//returnlist cannot be null	
	if (returnList == null)
	{
		throw new ArgumentNullException("No return List was supplied(null)");
	}

	//you can't return 0 item
	if (returnList.Count == 0)
	{
		errorList.Add(new Exception("There should be at least one returning item in the list."));
	}

	//    rule:    Only Associates or StoreManagers can do sales and returns
	if (employeeID <= 0)
	{
		throw new ArgumentNullException("Valid Employee ID should be supplied");
	}

	var checkPosition = Employees
								.Where(x => x.EmployeeID == employeeID && x.RemoveFromViewFlag == false)
								.FirstOrDefault();

	if (checkPosition == null) //checking matching employee
	{
		throw new ArgumentException($"There is no matching employee with ID {employeeID}.");
	}

	//with only certain roles, you can do return transaction.
	if (!(checkPosition.Position.Description == "Store Manager" || checkPosition.Position.Description == "Associate"))
	{
		errorList.Add(new Exception("This Employee is not allowed to do sales and returns transaction."));
	}


	#endregion

	int discount = 0; //this is discount percentage, default to 0.
					  //we don't edit salesrefund. we are always adding a NEW salerefunds record. so just initialize the object.

	SaleRefunds saleRefunds = new SaleRefunds();
	saleRefunds.SaleRefundDetails = new(); //initializing the list to prevent error.
	saleRefunds.SaleRefundDate = DateTime.Now;

	//extracting saleID(all return items have same saleID), and assigning it to saleRefunds object before adding to db.
	var saleID = returnList.Select(x => x.SaleID).FirstOrDefault();
	if (saleID.HasValue) //it is always gonna have value because it was prepared that way in the previous method.
	{
		saleRefunds.SaleID = saleID.Value;
	}
	saleRefunds.EmployeeID = employeeID;




	foreach (var item in returnList)
	{
		if (item.RtnQty > 0) //causing dbexception error when rtnqty is 0 and assigning it to the database.
		{
			SaleRefundDetails saleRefundDetails = new();
			saleRefundDetails.Quantity = item.RtnQty;
			saleRefundDetails.StockItemID = item.StockItemID;

			//talked about this with James. 
			//Here, DB is saving pre-discounted price(original price). But discount is definitely applied(if applicable) in edit return qty method to show customers in UI.
			saleRefundDetails.SellingPrice = item.SellingUnitPrice;
			saleRefunds.SaleRefundDetails.Add(saleRefundDetails);
		}
	}

	saleRefunds.SubTotal = saleRefunds.SaleRefundDetails.Sum(a => a.Quantity * a.SellingPrice);
	saleRefunds.TaxAmount = saleRefunds.SubTotal * 0.05m;
	SaleRefunds.Add(saleRefunds);



	//applying discount(if applicable) to return price for showing customer.
	decimal Total = (saleRefunds.SubTotal + saleRefunds.TaxAmount) * (100 - matchingCouponRate) / 100;



	//from here update stock item
	foreach (var item in returnList)
	{
		var matchingStockItem = StockItems.Where(x => x.StockItemID == item.StockItemID &&
													x.Discontinued == false &&
													x.RemoveFromViewFlag == false).FirstOrDefault();
		if (matchingStockItem == null)
		{
			throw new ArgumentNullException("There is no matching Stock Item.");
		}

		//returned Items mean StockItem quantity should increase accordingly
		matchingStockItem.QuantityOnHand += item.RtnQty;

		StockItems.Update(matchingStockItem);
	}

	if (errorList.Count > 0)
	{
		//Clearing the "track changes" ensures consistency in our entity system.
		//Otherwise we leave our entity system in flux
		ChangeTracker.Clear();
		//Throw an AggregateException containing the list of business processing errors.
		throw new AggregateException("Failed to add SaleRefund. Please check erroor message(s)", errorList);
	}
	else
	{
		Console.WriteLine($"{Total} is your total return price (discount considered if applicable).");
		SaveChanges();
		return saleRefunds.SaleRefundID;
	}
}



#endregion




// This region includes the view models used to
// represent and structure data for the UI.
#region View Models

public class CategoryView
{
	public int CategoryID { get; set; }
	public string Description { get; set; }
	public int NumberOfItems { get; set; }

}

public class ShoppingItemsView
{
	public int ProductID { get; set; }
	public string ProductName { get; set; }
	public int Quantity { get; set; } = 1;
	public decimal UnitPrice { get; set; }
	public decimal Subtotal => UnitPrice * Quantity;
}

private List<ShoppingItemsView> shoppingListItems = new List<ShoppingItemsView>();
private List<ReturnItemView> returnList = new List<ReturnItemView>();
private List<ReturnItemView> cloneforComparison = new();

private decimal subTotalOfReturnItems;
private decimal taxOfReturnItems;
private decimal totalReturn;
private bool isCouponUsed;
private decimal matchingCouponRate = 0;

public class StockItemView
{
	public int StockItemID { get; set; }
	public string Description { get; set; }
	public decimal SellingPrice { get; set; }


}


public class SalesView
{

	public int SaleID { get; set; }
	public DateTime SaleDate { get; set; }
	public string PaymentType { get; set; }
	public int EmployeeID { get; set; }
	public decimal TaxAmount { get; set; }
	public decimal SubTotal { get; set; }
	public int CouponID { get; set; }

}

public class ReturnItemView
{
	public int StockItemID { get; set; }
	public string Item { get; set; }
	public int? SaleID { get; set; }
	public int AvailQTY { get; set; }
	public decimal SellingUnitPrice { get; set; }
	public decimal OriginalPrice { get; set; }
	public int RtnQty { get; set; }
	public bool discountFlag { get; set; }
	public decimal discountRate { get; set; }
}

#endregion
