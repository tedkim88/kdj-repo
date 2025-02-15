<Query Kind="Program">
  <Connection>
    <ID>ab7ba83e-faf8-4470-bd6f-d1efa5447284</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>.</Server>
    <Database>eTools2023</Database>
    <DisplayName>eTools2023-Entity2</DisplayName>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>


void Main()
{
	//Set Postion that is allowed for access to the system
	int allowedPositionID = 4;

	//User Logs In
	int employeeID = 3;

	//Get employee info based on log in credentials
	var employee = TestGetEmployee(employeeID);


	if (employee != null && employee.PositionID == allowedPositionID)
	{
		#region Main Testing 

		Console.WriteLine("Select a Vendor:");
		TestGetVendors().Dump();

		//Users name is displayed based on login credentials
		Console.WriteLine($"Welcome: {employee.EmployeeName}");
		//Display selected vendor information
		TestGetPurchaseOrder(360).Dump();
		Console.WriteLine("Add more products to the order:");
		TestGetInventoryByVendor(2, 360).Dump();
		#endregion

		#region AddEdit Passing
		Console.WriteLine("=== Edit PurchaseOrder #360 - Pass ===");

		var passingPurchaseOrder = new PurchaseOrderView
		{
			PurchaseOrderID = 360,
			PurchaseOrderDetails = new List<PurchaseOrderDetailView>
			{
				new PurchaseOrderDetailView
				{
					StockItemID = 5581,
					QuantityToOrder = 35,
					Price = 129.65m
				}
			}
		};

		TestAddEditPurchaseOrder(passingPurchaseOrder).Dump();

		TestGetPurchaseOrder(360).Dump();
		#endregion

		#region AddEdit Failing Null
		Console.WriteLine("=== Edit PurchaseOrder #360 - Fail Null ===");

		var failingPurchaseOrderNull = new PurchaseOrderView
		{
			PurchaseOrderID = 360,
			PurchaseOrderDetails = new List<PurchaseOrderDetailView>
			{
				new PurchaseOrderDetailView
				{
					StockItemID = 5581,
					QuantityToOrder = 0,
					Price = 3
				}
			}
		};

		TestAddEditPurchaseOrder(null).Dump();
		#endregion

		#region AddEdit Failing Quantity
		Console.WriteLine("=== Edit PurchaseOrder #360 - Fail Quantity ===");

		var failingPurchaseOrderQuantity = new PurchaseOrderView
		{
			PurchaseOrderID = 360,
			PurchaseOrderDetails = new List<PurchaseOrderDetailView>
			{
				new PurchaseOrderDetailView
				{
					StockItemID = 5581,
					QuantityToOrder = 0,
					Price = 3
				}
			}
		};

		TestAddEditPurchaseOrder(failingPurchaseOrderQuantity).Dump();
		#endregion

		#region AddEdit Failing Price
		Console.WriteLine("=== Edit PurchaseOrder #360 - Fail Price ===");

		var failingPurchaseOrderPrice = new PurchaseOrderView
		{
			PurchaseOrderID = 360,
			PurchaseOrderDetails = new List<PurchaseOrderDetailView>
			{
				new PurchaseOrderDetailView
				{
					StockItemID = 5581,
					QuantityToOrder = 5,
					Price = -3
				}
			}
		};

		TestAddEditPurchaseOrder(failingPurchaseOrderPrice).Dump();
		#endregion

		#region Delete Testing
		//Display Purchase Order 350
		TestGetPurchaseOrder(350).Dump();
		
		//Delete Purchase Order
		TestDeletePurchaseOrder(350);

		//Try to Display again
		TestGetPurchaseOrder(350).Dump();
		#endregion
	}
	else
	{
		Console.WriteLine($"Access Denied: {employee.EmployeeName} is not authorized");
	}
}

#region Test Methods
public EmployeeView TestGetEmployee(int employeeID)
{
	try
	{
		return GetEmployee(employeeID);
	}

	#region catch all exception
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
	return null;  //  Ensure a valid return value even on failure
}

public List<VendorView> TestGetVendors()
{
	try
	{
		return GetVendors();
	}

	#region catch all exception
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
	return null;
}

public VendorView TestGetVendor(int vendorID)
{
	try
	{
		return GetVendor(vendorID);
	}

	#region catch all exception
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
	return null;
}

public PurchaseOrderView TestGetPurchaseOrder(int purchaseOrderID)
{
	try
	{
		return GetPurchaseOrder(purchaseOrderID);
	}

	#region catch all exception
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
	return null;
}

public List<StockView> TestGetInventoryByVendor(int vendorID, int purchaseOrderID)
{
	try
	{
		return GetInventoryByVendor(vendorID, purchaseOrderID);
	}

	#region catch all exception
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
	return null;
}

public PurchaseOrderView TestAddEditPurchaseOrder(PurchaseOrderView purchaseOrderView)
{
	try
	{
		AddEditPurchaseOrder(purchaseOrderView);
		return purchaseOrderView;
	}
	catch (AggregateException ex)
	{
		foreach (var error in ex.InnerExceptions)
		{
			Console.WriteLine(error.Message);
		}
	}
	catch (ArgumentNullException ex)
	{
		Console.WriteLine(GetInnerException(ex).Message);
	}
	catch (Exception ex)
	{
		Console.WriteLine(GetInnerException(ex).Message);
	}
	return null;
}

public EmployeeView TestDeletePurchaseOrder(int purchaseOrderID)
{
	try
	{
		DeletePurchaseOrder(purchaseOrderID);
	}

	#region catch all exception	
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
	return null;  //  Ensure a valid return value even on failure
}

#endregion

#region Support Methods

public Exception GetInnerException(System.Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;


}

#endregion

#region Methods
public List<VendorView> GetVendors()
{
	#region Business Logic and Parameter Exceptions
	List<Exception> errorList = new List<Exception>();

	#endregion

	return Vendors
			.Where(x => x.RemoveFromViewFlag == false)
			.Select(x => new VendorView
			{
				VendorName = x.VendorName,
				Address = x.Address
			})
			.OrderBy(x => x.VendorName)
			.ToList();
}

public VendorView GetVendor(int vendorID)
{
	#region Business Logic and Parameter Exceptions

	List<Exception> errorList = new List<Exception>();

	if (vendorID <= 0)
	{
		throw new ArgumentNullException("Invalid vendorID!");
	}
	#endregion


	return Vendors
			.Where(x => x.VendorID == vendorID)
			.Select(x => new VendorView
			{
				VendorName = x.VendorName,
				Address = x.Address
			}).FirstOrDefault();
}

public EmployeeView GetEmployee(int employeeID)
{
	#region Business Logic and Parameter Exceptions
	List<Exception> errorList = new List<Exception>();

	if (employeeID <= 0)
	{
		throw new ArgumentNullException("Invalid employeeID!");
	}
	#endregion


	return Employees
			.Where(x => x.EmployeeID == employeeID && x.RemoveFromViewFlag == false)
			.Select(x => new EmployeeView
			{
				EmployeeName = x.FirstName + " " + x.LastName,
				PositionID = x.PositionID,
				PositionDescription = x.Position.Description

			}).FirstOrDefault();
}

public PurchaseOrderView GetPurchaseOrder(int purchaseOrderID)
{
	if (purchaseOrderID == 0)
	{
		throw new ArgumentNullException("Please provide a purchase order id");
	}

	var purchaseOrder = PurchaseOrders
		.Where(x => x.PurchaseOrderID == purchaseOrderID && !x.RemoveFromViewFlag)
		.Select(x => new PurchaseOrderView
		{
			PurchaseOrderID = x.PurchaseOrderID,
			OrderDate = x.OrderDate,
			Vendor = x.Vendor.VendorName,
			Phone = x.Vendor.Phone,
			Address = x.Vendor.Address,
			PostalCode = x.Vendor.PostalCode,
			City = x.Vendor.City,
			PurchaseOrderDetails = x.PurchaseOrderDetails
				.Where(pod => pod.RemoveFromViewFlag == false && pod.StockItem.VendorID == x.VendorID)
				.Select(pod => new PurchaseOrderDetailView
				{
					StockItemID = pod.StockItemID,
					Description = pod.StockItem.Description,
					QuantityOnHand = pod.StockItem.QuantityOnHand,
					ReorderLevel = pod.StockItem.ReOrderLevel,
					QuantityOnOrder = pod.StockItem.QuantityOnOrder,
					QuantityToOrder = pod.Quantity,
					Price = pod.PurchasePrice,
					OriginalPrice = pod.StockItem.SellingPrice,
					RemoveFromViewFlag = pod.RemoveFromViewFlag
				}).ToList()
		}).FirstOrDefault();

	if (purchaseOrder != null)
	{
		purchaseOrder.SubTotal = purchaseOrder.PurchaseOrderDetails.Sum(pod => pod.QuantityToOrder * pod.Price);
		purchaseOrder.GST = purchaseOrder.SubTotal * 0.05m;
		purchaseOrder.Total = purchaseOrder.SubTotal + purchaseOrder.GST;
	}

	return purchaseOrder;
}

public List<StockView> GetInventoryByVendor(int vendorID, int purchaseOrderID)
{
	#region Business Logic and Parameter Exceptions
	List<Exception> errorList = new List<Exception>();

	#endregion

	//Rule:
	//Only display stock items that are not already in the purchase order.
	var itemsInPurchaseOrder = PurchaseOrders
		.Where(po => po.PurchaseOrderID == purchaseOrderID)
		.SelectMany(po => po.PurchaseOrderDetails)
		.Select(pod => pod.StockItemID)
		.ToList();

	return StockItems
			.Where(x => x.VendorID.Equals(vendorID) &&
						x.RemoveFromViewFlag.Equals(false) &&
						!itemsInPurchaseOrder.Contains(x.StockItemID))
			.Select(x => new StockView
			{
				StockItemID = x.StockItemID,
				Description = x.Description,
				QuantityOnHand = x.QuantityOnHand,
				ReorderLevel = x.ReOrderLevel,
				QuantityOnOrder = x.QuantityOnOrder,
				Buffer = x.QuantityOnHand - x.ReOrderLevel,
				Price = x.SellingPrice
			})
			.OrderBy(x => x.Description)
			.ToList();
}

public void AddEditPurchaseOrder(PurchaseOrderView purchaseOrderView)
{
	#region Business Logic and Parameter Exceptions
	List<Exception> errorList = new List<Exception>();

	if (purchaseOrderView == null)
	{
		throw new ArgumentNullException("No purchase order data provided.");
	}

	if (purchaseOrderView.PurchaseOrderDetails == null || purchaseOrderView.PurchaseOrderDetails.Count == 0)
	{
		errorList.Add(new Exception("At least one purchase order detail is required for a purchase order."));
	}

	var purchaseOrder = PurchaseOrders
		.Where(p => p.PurchaseOrderID == purchaseOrderView.PurchaseOrderID)
		.FirstOrDefault();

	if (purchaseOrder == null)
	{
		throw new ArgumentException("Purchase order does not exist.");
	}
	#endregion

	foreach (var purchaseOrderDetailView in purchaseOrderView.PurchaseOrderDetails)
	{
		var purchaseOrderDetails = purchaseOrder.PurchaseOrderDetails.Where(pod => pod.StockItemID == purchaseOrderDetailView.StockItemID).FirstOrDefault();
		if (purchaseOrderDetailView.QuantityToOrder <= 0)
		{
			errorList.Add(new Exception($"Quantity to order must be greater than 0"));
		}

		if (purchaseOrderDetailView.Price < 0)
		{
			errorList.Add(new Exception($"Price cannot be less than 0"));
		}

		if (purchaseOrderDetails == null)
		{
			purchaseOrderDetails = new PurchaseOrderDetails
			{
				PurchaseOrderID = purchaseOrder.PurchaseOrderID,
				StockItemID = purchaseOrderDetailView.StockItemID,
				Quantity = purchaseOrderDetailView.QuantityToOrder,
				PurchasePrice = purchaseOrderDetailView.Price
			};
			purchaseOrder.PurchaseOrderDetails.Add(purchaseOrderDetails);
		}
		else
		{
			purchaseOrderDetails.Quantity = purchaseOrderDetailView.QuantityToOrder;
			purchaseOrderDetails.PurchasePrice = purchaseOrderDetailView.Price;
		}
	}

	purchaseOrder.SubTotal = purchaseOrder.PurchaseOrderDetails
		.Where(pod => pod.RemoveFromViewFlag.Equals(false))
		.Sum(pod => pod.Quantity * pod.PurchasePrice);

	purchaseOrder.TaxAmount = purchaseOrder.SubTotal * 0.05m;

	purchaseOrderView.Total = purchaseOrder.SubTotal + purchaseOrder.TaxAmount;

	if (errorList.Count > 0)
	{
		ChangeTracker.Clear();
		throw new AggregateException("Failed to add or edit purchase order. Check errors.", errorList);
	}
	else
	{
		if (purchaseOrder.PurchaseOrderID == 0)
		{
			//Rule:
			//Vendor can only have one active Purchase order
			if (PurchaseOrders.Count(p => p.Closed == false) > 1)
			{
				PurchaseOrders.Add(purchaseOrder);
			}
			else
			{
				throw new InvalidOperationException("There can only be 1 active purchase order per vendor");
			}

		}
		else
		{
			PurchaseOrders.Update(purchaseOrder);
		}
		SaveChanges();
	}
}

public void DeletePurchaseOrder(int purchaseOrderID)
{
	List<Exception> errorList = new List<Exception>();
	PurchaseOrders purchaseOrder = PurchaseOrders.Where(x => x.PurchaseOrderID == purchaseOrderID).FirstOrDefault();
	if (purchaseOrderID.Equals(null))
	{
		errorList.Add(new Exception("No purchase order id provided."));
	}

	if (errorList.Count > 0)
	{
		ChangeTracker.Clear();
		throw new AggregateException("Failed to delete. Check errors.", errorList);
	}
	else
	{
		purchaseOrder.RemoveFromViewFlag = true;
		SaveChanges();
	}


}
#endregion

#region View Models
public class EmployeeView
{
	public int EmployeeID { get; set; }
	public string EmployeeName { get; set; }
	public int PositionID { get; set; }
	public string PositionDescription { get; set; }
	public bool RemoveFromViewFlag { get; set; }
}

public class VendorView
{
	//public int VendorID {get; set;}
	public string VendorName { get; set; }
	public string Address { get; set; }
	public bool RemoveFromViewFlag { get; set; }
}

public class PurchaseOrderView
{
	public int PurchaseOrderID { get; set; }
	public DateTime? OrderDate { get; set; }
	//public int VendorID {get; set;}
	//public int EmployeeID {get; set;}
	public string Vendor { get; set; }
	public string Phone { get; set; }
	public string Address { get; set; }
	public string City { get; set; }
	public string PostalCode { get; set; }
	public List<PurchaseOrderDetailView> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetailView>();
	public decimal SubTotal { get; set; }
	public decimal GST { get; set; }
	public decimal Total { get; set; }
	public bool RemoveFromViewFlag { get; set; }
}

public class PurchaseOrderDetailView
{
	//public int PurchaseOrderDetailID {get; set;}
	//public int PurchaseOrderID {get; set;}
	public int StockItemID { get; set; }
	public string Description { get; set; }
	public int QuantityOnHand { get; set; }
	public int ReorderLevel { get; set; }
	public int QuantityOnOrder { get; set; }
	public int QuantityToOrder { get; set; }
	public decimal Price { get; set; }
	public decimal OriginalPrice { get; set; }
	public bool RemoveFromViewFlag { get; set; }
}

public class StockView
{
	public int StockItemID { get; set; }
	//public int VendorID { get; set; }
	public string Description { get; set; }
	public int QuantityOnHand { get; set; }
	public int ReorderLevel { get; set; }
	public int QuantityOnOrder { get; set; }
	public int Buffer { get; set; }
	public decimal Price { get; set; }
	public bool RemoveFromViewFlag { get; set; }
}
#endregion