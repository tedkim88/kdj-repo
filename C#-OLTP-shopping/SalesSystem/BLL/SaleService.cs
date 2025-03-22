using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SalesSystem.DAL;
using SalesSystem.Entities;
using SalesSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SalesSystem.BLL
{
    public class SaleService
    {
        #region Fields

        /// <summary>
        /// The Sales context
        /// 
        /// </summary>
        bool isCouponUsed;
        int matchingCouponRate = 0;
        private List<ShoppingItemsView> shoppingListItems = new List<ShoppingItemsView>();
        private readonly SalesContext _salesContext;
        public List<ReturnItemView> returnList = new();
        private readonly RefundService _refundService;
        #endregion


        internal SaleService(SalesContext salesContext, RefundService refundService)
        {

            _salesContext = salesContext;
            _refundService = refundService;
        }



        public List<ReturnItemView> GetSaleRefund(int? saleID)
        {
            #region Business Logic and Parameter Exceptions
          
            List<Exception> errorList = new List<Exception>();

         

            isCouponUsed = false; 
            matchingCouponRate = 0; 

            //checking validity of saleid
            if (saleID == null || saleID <= 0)
            {
                throw new ArgumentException("Invalid SaleID. Valid SaleID is required for returning.");
            }

            var matchingSaleDetail = _salesContext.SaleDetails
                                            .Where(x => x.SaleID == saleID && x.RemoveFromViewFlag == false)
                                            .FirstOrDefault();

            //check if the customer has actually purchased something.
            if (matchingSaleDetail == null)
            {
                throw new ArgumentException("There is no matching sale Detail with the saleID.");
            }


            var matchingSale = _salesContext.Sales.Where(x => x.SaleID == saleID && x.RemoveFromViewFlag == false).FirstOrDefault();
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

            Console.WriteLine($"Coupon used?(T/F):{isCouponUsed}. for this saleID {saleID}.");

            #endregion

            var matchingSaleDetails = _salesContext.SaleDetails
                                            .Where(x => x.SaleID == saleID &&
                                                        x.RemoveFromViewFlag == false)
                                            .ToList();



            //let's use this variable if coupon has been applied to each item with a certain discount rate.
            matchingCouponRate = _salesContext.Sales.Where(x => x.SaleID == saleID && x.CouponID != null && x.RemoveFromViewFlag == false)
                                        .Select(x => x.Coupon.CouponDiscount)
                                        .FirstOrDefault();

            Console.WriteLine($"{matchingCouponRate} is matching CouponRate Percentage");






            returnList = _salesContext.SaleDetails
                                            .Where(x => x.SaleID == saleID &&
                                                        x.RemoveFromViewFlag == false)
                                .Select(x => new ReturnItemView
                                {
                                    StockItemID = x.StockItemID,
                                    Item = x.StockItem.Description,
                                    SaleID = x.SaleID,
                                    AvailQTY = x.Quantity,
                                    OriginalPrice = x.SellingPrice * x.Quantity, //this is total price paid for each item.
                                    SellingUnitPrice = x.SellingPrice,
                                    RtnQty = _refundService.GetReturnedQty(x.SaleID, x.StockItemID),
                                    QtyToRtn = 0,
                                    discountFlag = isCouponUsed,
                                    discountRate = matchingCouponRate
                                }).ToList();



            return returnList;
        }




        public int AddSales(AppState appState, int employeeID, string couponIDValue, string paymentType, int saleID)
        {
            #region Business Logic and Parameter Exceptions
            //    create a list<Exception> to contain all discovered errors
            List<Exception> errorList = new List<Exception>();
            //  Business Rules
            //    These are processing rules that need to be satisfied
            //        for valid data
            //    rule:    shoppingItemView cannot be null
            if (appState == null)
            {
                throw new ArgumentNullException("No shopping list was supplied");
            }

            //when adding sales, the list should have at least one item
            if (appState.cartView.Count == 0)
            {
                errorList.Add(new Exception("There should be at least one shopping item in the list."));
            }

            if (appState.cartView.Any(x => x.Quantity <= 0))
            {
                errorList.Add(new Exception("Please Remove item of quantity 0 or less."));
            }

            //checking employeeID validity
            if (employeeID <= 0)
            {
                errorList.Add(new Exception("Invalid Employee ID! should be greater than 0."));
            }


            var checkPosition = _salesContext.Employees
                                        .Where(x => x.EmployeeID == employeeID && x.RemoveFromViewFlag == false)
                                        .Include(x => x.Position)
                                        .FirstOrDefault();

            //there is no matching Employee in DB
            if (checkPosition == null) //checking matching employee
            {
                throw new ArgumentException($"There is no matching employee with ID {employeeID}.");
            }

            //checking positions. only certain roles can do this transaction.
            if (checkPosition.Position == null)
            {
                throw new ArgumentException($"This employee doesn't have any position.");
            }

            //이렇게 점찍고 다음 연결된 필드로 넘어갈때마다 null체크가 필요한듯..바로 description으로 넘어가기는 어려움
            if (!(checkPosition.Position.Description == "Store Manager" || checkPosition.Position.Description == "Associate"))
            {
                errorList.Add(new Exception("This Employee is not allowed to do sales and returns transaction."));
            }

            int discount = 0; //this is discount percentage, default to 0.

            //checking coupon
            var matchingCoupon = _salesContext.Coupons
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
                errorList.Add(new Exception("Select your Payment Type."));
            }

            #endregion


            Sale sale = _salesContext.Sales
                            .Where(x => x.SaleID == saleID)
                            .FirstOrDefault();


           
            if (sale != null) 
            {
                throw new ArgumentNullException("You can't change already existing sale Record.");
            }


            sale = new Sale();
            sale.SaleDate = DateTime.Now;
            sale.SaleDetails = new List<SaleDetail>();
            sale.EmployeeID = employeeID;
            sale.PaymentType = upperPaymentType;
            sale.CouponID = matchingCoupon == null ? null
                            : CouponExpired == true ? null : matchingCoupon.CouponID;

            sale.SubTotal = appState.cartView.Sum(a => a.Subtotal); 
            sale.TaxAmount = sale.SubTotal * 0.05m; 

         

            decimal Total = sale.SubTotal * (100 - discount) / 100 + sale.TaxAmount * (100 - discount) / 100; 




           
            foreach (var item in appState.cartView)
            {
                SaleDetail saleDetails = new();
                saleDetails.StockItemID = item.ProductID;
                saleDetails.SellingPrice = item.UnitPrice;
                saleDetails.Quantity = item.Quantity;
                sale.SaleDetails.Add(saleDetails);
            }

            _salesContext.Sales.Add(sale);

           
            foreach (var item in appState.cartView)
            {
                var matchingStockItem = _salesContext.StockItems.Where(x => x.StockItemID == item.ProductID &&
                                                            x.RemoveFromViewFlag == false &&
                                                            x.Discontinued == false)
                                                            .FirstOrDefault();
                if (matchingStockItem == null)
                {
                    throw new ArgumentNullException("There is no matching Stock Item.");
                }

                if (item.Quantity > matchingStockItem.QuantityOnHand)
                {
                    errorList.Add(new Exception($"You can't buy more than quantity on hand. Current Quantity for item {item.ProductName} is {matchingStockItem.QuantityOnHand}"));
                }
                else
                {
                    matchingStockItem.QuantityOnHand -= item.Quantity;
                    _salesContext.StockItems.Update(matchingStockItem);
                }
            }

            if (errorList.Count > 0)
            {
             
                _salesContext.ChangeTracker.Clear();
               
                throw new AggregateException("Failed to Add Sales. Please check error message(s)", errorList);
            }
            else
            {
                Console.WriteLine($"{Total} is total price including tax with discount applied(if applicable)"); //just for checking in linqpad
                _salesContext.SaveChanges();
                return sale.SaleID;
            }

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


            if (!(returnList.Any(a => a.QtyToRtn != 0)))
            {
                errorList.Add(new Exception("Double check if you have clicked change qty to confirm return amount."));
            }

            //    rule:    Only Associates or StoreManagers can do sales and returns
            if (employeeID <= 0)
            {
                throw new ArgumentNullException("Valid Employee ID should be supplied");
            }

            var checkPosition = _salesContext.Employees
                                        .Include(x => x.Position)
                                        .Where(x => x.EmployeeID == employeeID && x.RemoveFromViewFlag == false)
                                        .FirstOrDefault();

            if (checkPosition == null) //checking matching employee
            {
                throw new ArgumentException($"There is no matching employee with ID {employeeID}.");
            }

            if (checkPosition.Position == null) //checking matching employee
            {
                throw new ArgumentException($"This employee doesn't have any position.");
            }

            //with only certain roles, you can do return transaction.
            if (!(checkPosition.Position.Description == "Store Manager" || checkPosition.Position.Description == "Associate"))
            {
                errorList.Add(new Exception("This Employee is not allowed to do sales and returns transaction."));
            }


            #endregion

            int discount = 0; 

            SaleRefund saleRefunds = new SaleRefund();
            saleRefunds.SaleRefundDetails = new List<SaleRefundDetail>();
            saleRefunds.SaleRefundDate = DateTime.Now;

          
            var saleID = returnList.Select(x => x.SaleID).FirstOrDefault();
            if (saleID.HasValue) 
            {
                saleRefunds.SaleID = saleID.Value;
            }
            saleRefunds.EmployeeID = employeeID;




            foreach (var item in returnList)
            {
                if (item.QtyToRtn > 0) 
                {
                    SaleRefundDetail saleRefundDetails = new();
                    saleRefundDetails.Quantity = item.QtyToRtn;
                    saleRefundDetails.StockItemID = item.StockItemID;

                  
                    saleRefundDetails.SellingPrice = item.SellingUnitPrice;
                    saleRefunds.SaleRefundDetails.Add(saleRefundDetails);
                }
            }

            saleRefunds.SubTotal = saleRefunds.SaleRefundDetails.Sum(a => a.Quantity * a.SellingPrice);
            saleRefunds.TaxAmount = saleRefunds.SubTotal * 0.05m;
            _salesContext.SaleRefunds.Add(saleRefunds);



           
            decimal Total = (saleRefunds.SubTotal + saleRefunds.TaxAmount) * (100 - matchingCouponRate) / 100;



           
            foreach (var item in returnList)
            {
                var matchingStockItem = _salesContext.StockItems.Where(x => x.StockItemID == item.StockItemID &&
                                                            x.Discontinued == false &&
                                                            x.RemoveFromViewFlag == false).FirstOrDefault();
                if (matchingStockItem == null)
                {
                    throw new ArgumentNullException("There is no matching Stock Item.");
                }

               
                matchingStockItem.QuantityOnHand += item.QtyToRtn;

                _salesContext.StockItems.Update(matchingStockItem);
            }

            if (errorList.Count > 0)
            {
              
                _salesContext.ChangeTracker.Clear();
              
                throw new AggregateException("Failed to add SaleRefund. Please check erroor message(s)", errorList);
            }
            else
            {
                Console.WriteLine($"{Total} is your total return price (discount considered if applicable).");
                _salesContext.SaveChanges();
                return saleRefunds.SaleRefundID;
            }
        }
    }






}

