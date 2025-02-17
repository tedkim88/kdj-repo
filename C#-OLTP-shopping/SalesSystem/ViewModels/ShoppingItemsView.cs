using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.ViewModels
{
    public class ShoppingItemsView
    {
      
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; } = 1;
            public decimal UnitPrice { get; set; }
            public decimal Subtotal => UnitPrice * Quantity;
        
    }
}
