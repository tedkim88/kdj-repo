<Query Kind="Program">
  <Connection>
    <ID>b72fe304-2534-4c87-846e-df55280fb602</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>eTools2023</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	// placeholder for pass or fail
	string passFail = string.Empty;

	#region FetchOutstandingOrders
	Console.WriteLine("================================");
	Console.WriteLine("======Fetch Outstanding Order=====");
	Console.WriteLine("================================");
	Console.WriteLine();
	TestFetchOutstandingOrders().Dump();
	#endregion


	#region FetchOrderDetails
	Console.WriteLine("=============================");
	Console.WriteLine("===== Fetch Order Details======");
	Console.WriteLine("=============================");
	Console.WriteLine();
	TestFetchOrderDetails(350).Dump();
	TestFetchOrderDetails(200).Dump("Pass - Valid ID entered, no order found");
	#endregion


	#region ProcessReceivedPurchaseOrder
	Console.WriteLine("======================================");
	Console.WriteLine("=====Process Received Purchase Order====");
	Console.WriteLine("=======================================");
	Console.WriteLine("Processed Order Details:");
	Console.WriteLine();
	TestFetchOrderDetails(350).Dump();
	#endregion


	#region ForceCloseOrder
	Console.WriteLine("====================================");
	Console.WriteLine("=======Force Close Order============");
	Console.WriteLine("====================================");
	Console.WriteLine();
	ForceCloseOrder(350, "Test");
	TestFetchOrderDetails(350).Dump();
	#endregion
}

// support method 

#region Support Methods
public Exception GetInnerException(System.Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}
#endregion

// test method 
#region Test Methods
public List<PurchaseOrderView> TestFetchOutstandingOrders()
{
	try
	{
		return FetchOutstandingOrders();
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

public PurchaseOrderView TestFetchOrderDetails(int purchaseOrderID)
{
	try

	{
		return FetchOrderDetails(purchaseOrderID);
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
#endregion

// methods 

#region Methods 

public List<PurchaseOrderView> FetchOutstandingOrders()
{

	return PurchaseOrders.Where(p => p.Closed == false && p.OrderDate.HasValue && p.RemoveFromViewFlag == false)
	.ToList()
	.Select(p => new PurchaseOrderView
	{

		PurchaseOrderID = p.PurchaseOrderID,
		OrderDate = p.OrderDate,
		VendorID = p.VendorID,
		EmployeeID = p.EmployeeID,
		TaxAmount = p.TaxAmount,
		SubTotal = p.SubTotal,
		Notes = p.Notes,
		RemoveFromViewFlag = p.RemoveFromViewFlag,
		VendorName = p.Vendor.VendorName,
		VendorPhone = p.Vendor.Phone,
		Closed = p.Closed,
		PurchaseOrderDetails = p.PurchaseOrderDetails

		.Select(g => new PurchaseOrderDetailsView

		{
			PurchaseOrderDetailID = g.PurchaseOrderDetailID,
			PurchaseOrderID = g.PurchaseOrderID,
			StockItemID = g.StockItemID,
			PurchasePrice = g.PurchasePrice,
			Quantity = g.Quantity,
			QuantityOutstanding = (g.Quantity - g.ReceiveOrderDetails.Sum(s => s.QuantityReceived)),
			StockItemDescription = g.StockItem.Description,
			ReceivedQuantity = g.ReceiveOrderDetails.Sum(s => s.QuantityReceived),
			ReturnedQuantity = g.ReturnedOrderDetails.Sum(s => s.Quantity),
			ReturnReason = g.ReturnedOrderDetails.Any() ? string.Join(",", g.ReturnedOrderDetails.Select(s => s.Reason)) : "null"

		})
		.ToList()
	})
	.ToList();

}

public PurchaseOrderView FetchOrderDetails(int purchaseOrderID)
{
	PurchaseOrderView order = PurchaseOrders.Where(p => p.PurchaseOrderID == purchaseOrderID)
	.ToList()
	.Select(p => new PurchaseOrderView

	{
		PurchaseOrderID =  p.PurchaseOrderID,
		OrderDate =  p.OrderDate,
		VendorID =  p.VendorID,
		EmployeeID =  p.EmployeeID,
		TaxAmount =  p.TaxAmount,
		SubTotal =  p.SubTotal,
		Closed =  p.Closed,
		Notes =  p.Notes,
		RemoveFromViewFlag =  p.RemoveFromViewFlag,
		VendorName =  p.Vendor.VendorName,
		VendorPhone =  p.Vendor.Phone,
		PurchaseOrderDetails =  p.PurchaseOrderDetails

		 .Select(g => new PurchaseOrderDetailsView
		 {
		 	
			 PurchaseOrderDetailID =  g.PurchaseOrderDetailID,
			 PurchaseOrderID =  g.PurchaseOrderID,
			 StockItemID =  g.StockItemID,
			 PurchasePrice =  g.PurchasePrice,
			 Quantity =  g.Quantity,
			 QuantityOutstanding =  (g.Quantity - g.ReceiveOrderDetails.Sum(s => s.QuantityReceived)),
			 StockItemDescription =  g.StockItem.Description,
			 ReceivedQuantity =  g.ReceiveOrderDetails.Sum(s => s.QuantityReceived),
			 ReturnedQuantity =  g.ReturnedOrderDetails.Sum(s => s.Quantity),
			 ReturnReason =  g.ReturnedOrderDetails.Any()? string.Join(",", g.ReturnedOrderDetails.Select(s => s.Reason)): "null"
			 
		 })
				.ToList()
	})
	
		.FirstOrDefault();

	return order;
	
}

public void ProcessReceivedPurchaseOrder(int purchaseOrderID,
	List<ReceiveOrderDetailsView> receiveOrderDetails,
	List<ReturnedOrderDetailsView> returnedItems,
	List<UnOrderedItemsView> unorderedItems)
{
	// Find the purchase order

	var purchaseOrder = PurchaseOrders.FirstOrDefault(p => p.PurchaseOrderID == purchaseOrderID);
	if (purchaseOrder == null)
	{
		throw new Exception("Order ID not found");
		
	}

	// received items
	if (receiveOrderDetails.Any(item => item.QuantityReceived < 0))
	{
		throw new Exception("Received quantity must be non-negative");
		
	}

	// returned items
	if (returnedItems.Any(item => item.Quantity < 0 || string.IsNullOrEmpty(item.Reason)))
	{
		throw new Exception("Invalid data for returned items");
		
	}

	// unordered items
	if (unorderedItems.Any(item => string.IsNullOrWhiteSpace(item.ItemName) || item.Quantity <= 0 || string.IsNullOrWhiteSpace(item.VendorStockNumber)))
	{
		throw new Exception("Invalid data for unordered items");
		
	}

	Console.WriteLine($"Order {purchaseOrderID} processed successfully");
}

public void ForceCloseOrder(int purchaseOrderID, string reason)
{
	// purchase order
	var purchaseOrder = PurchaseOrders.FirstOrDefault(p => p.PurchaseOrderID == purchaseOrderID);
	if (purchaseOrder == null)
	{
		throw new Exception("Order ID not found");
		
	}

	// Check if order is already closed
	if (purchaseOrder.Closed)
	{
		Console.WriteLine("Order is already closed");
		
	}

	// closed status and add reason  
	purchaseOrder.Closed = true;
	purchaseOrder.Notes = string.IsNullOrWhiteSpace(reason)
		? purchaseOrder.Notes
		: $"{purchaseOrder.Notes} | {reason}";

	SaveChanges();
	Console.WriteLine($"Order {purchaseOrderID} closed. Reason: {reason}");
}

public void SaveChanges()
{
	Console.WriteLine("Changes saved successfully");
}



#endregion

// view Models 
#region View Models

public class PurchaseOrderView
{
	public int PurchaseOrderID { get; set; }
	public DateTime? OrderDate { get; set; }
	public int VendorID { get; set; }
	public int EmployeeID { get; set; }
	public decimal TaxAmount { get; set; }
	public decimal SubTotal { get; set; }
	public bool Closed { get; set; }
	public string Notes { get; set; }
	public bool RemoveFromViewFlag { get; set; }
	public string VendorName { get; set; }
	public string VendorPhone { get; set; }
	public List<PurchaseOrderDetailsView> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetailsView>();

}


public class PurchaseOrderDetailsView
{
	public int PurchaseOrderDetailID { get; set; }
	public int PurchaseOrderID { get; set; }
	public int StockItemID { get; set; }
	public decimal PurchasePrice { get; set; }
	public int Quantity { get; set; }
	public bool RemoveFromViewFlag { get; set; }
	public string StockItemDescription { get; set; }
	public int QuantityOutstanding { get; set; }
	public int ReceivedQuantity { get; set; }
	public int ReturnedQuantity { get; set; }
	public string ReturnReason { get; set; }

}


public class ReceiveOrdersView
{
	public int ReceiveOrderID { get; set; }
	public int PurchaseOrderID { get; set; }
	public DateTime? ReceiveDate { get; set; }
	public int? EmployeeID { get; set; }
	public bool RemoveFromViewFlag { get; set; }
	public List<ReceiveOrderDetailsView> ReceiveOrderDetails { get; set; } = new List<ReceiveOrderDetailsView>();
	public List<UnOrderedItemsView> UnorderedItems { get; set; } = new List<UnOrderedItemsView>();
}


public class ReceiveOrderDetailsView
{
	public int ReceiveOrderDetailID { get; set; }
	public int ReceiveOrderID { get; set; }
	public int PurchaseOrderDetailID { get; set; }
	public int QuantityReceived { get; set; }
	public bool RemoveFromViewFlag { get; set; }
}


public class UnOrderedItemsView
{
	public int UnOrderedItemID { get; set; }
	public int RecieveOrderID { get; set; }
	public int ItemID { get; set; }
	public string ItemName { get; set; }
	public string VendorProductID { get; set; }
	public int Quantity { get; set; }
	public bool RemoveFromViewFlag { get; set; }
	public string VendorStockNumber { get; set; }
}


public class ReturnedOrderDetailsView
{
	public int ReturnedOrderDetailID { get; set; }
	public int ReceiveOrderID { get; set; }
	public int? PurchaseOrderDetailID { get; set; }
	public int? UnOrderedItemID { get; set; }
	public string ItemDescription { get; set; }
	public int Quantity { get; set; }
	public string Reason { get; set; }
	public string VendorStockNumber { get; set; }
	public bool RemoveFromViewFlag { get; set; }
}



#endregion





