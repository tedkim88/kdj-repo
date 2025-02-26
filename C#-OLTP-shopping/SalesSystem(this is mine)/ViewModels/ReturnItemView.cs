using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.ViewModels
{
    public class ReturnItemView
    {
        public int StockItemID { get; set; }
        public string Item { get; set; }
        public int? SaleID { get; set; }
        public int AvailQTY { get; set; }
        public decimal SellingUnitPrice { get; set; }
        public decimal OriginalPrice { get; set; }

        public int QtyToRtn{ get; set; } //newly added..I was confused with this term in deliverable 2.

        public int RtnQty { get; set; }
        public bool discountFlag { get; set; }
        public decimal discountRate { get; set; }
        public int qtyPlaceholder { get; set; }
    }


}
