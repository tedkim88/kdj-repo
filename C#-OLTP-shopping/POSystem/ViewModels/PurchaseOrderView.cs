using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSystem.ViewModels
{
    [Table("PurchaseOrders")]
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
}
