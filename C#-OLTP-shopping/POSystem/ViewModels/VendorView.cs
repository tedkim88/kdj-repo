using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSystem.ViewModels
{
    public class VendorView
    {
        public int VendorID {get; set;}
        public string VendorName { get; set; }
        public string Address { get; set; }
        public bool RemoveFromViewFlag { get; set; }
    }
}
