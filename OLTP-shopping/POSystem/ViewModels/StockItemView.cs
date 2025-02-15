using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSystem.ViewModels
{
    public class StockItemView
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
}
