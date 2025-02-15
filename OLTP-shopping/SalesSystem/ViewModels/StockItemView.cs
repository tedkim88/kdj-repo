using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.ViewModels
{
    public class StockItemView
    {
        public int StockItemID { get; set; }
        public string Description { get; set; }
        public decimal SellingPrice { get; set; }

        //for cart
        public int Quantity { get; set; } = 1;

        //for checking qty on hand
        public int QuantityOnHand { get; set; }
    }
}
