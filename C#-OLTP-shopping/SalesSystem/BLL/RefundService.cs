using SalesSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.BLL
{
    public class RefundService
    {

        private readonly SalesContext _salesContext;

        internal RefundService(SalesContext salesContext)
        {
            
            _salesContext = salesContext;
        }


        public int GetReturnedQty(int saleid, int stockitemid)
        {
            return _salesContext.SaleRefundDetails
                              .Where(x => x.SaleRefund.SaleID == saleid && x.StockItemID == stockitemid)
                              .Sum(x => x.Quantity);
        }


    }
}
