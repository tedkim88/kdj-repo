using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesSystem.BLL;
using SalesSystem.DAL;
using SalesSystem.Entities;
using SalesSystem.ViewModels;

namespace SalesSystem.BLL
{
    public class CategoryService
    {

        #region Fields
     
        private List<ShoppingItemsView> shoppingListItems = new List<ShoppingItemsView>();
        private readonly SalesContext _salesContext;

        #endregion


        internal CategoryService(SalesContext salesContext)
        {
          
            _salesContext = salesContext;

        }

        #region Methods

        //get all categories
        public List<CategoryView> GetCategories()
        {

            return _salesContext.Categories
                                        .Where(x => x.RemoveFromViewFlag == false)
                                        .Select(x => new CategoryView
                                        {
                                            CategoryID = x.CategoryID,
                                            Description = x.Description,
                                            NumberOfItems = x.StockItems.Where(a => a.Discontinued == false && a.RemoveFromViewFlag == false)
                                                                       .Count()

                                        })
                                        .OrderBy(x => x.Description)
                                        .ToList();

        }

        //GetItemsByCategoryID
        public List<StockItemView> GetItemsByCategoryID(int? categoryID)
        {

            #region Business Logic and Parameter Exceptions
            //create a list<Exception> to contain all discovered errors
            List<Exception> errorList = new List<Exception>();

            //Business Rules
            //These are processing rules that need to be satisfied
            // for valid data
            // rule : categoryID should be valid(greater than 0)

            if(categoryID == null)
            {
                throw new ArgumentNullException("Category ID cannot be null.");
            }

            if (categoryID <= 0)
            {
                throw new ArgumentException("Category ID is invalid(should be greater than 0)");
            }

            var matchingCategoryID = _salesContext.Categories
                                            .Where(x => x.CategoryID == categoryID)
                                            .FirstOrDefault();

            if (matchingCategoryID == null) //maybe this is not needed because customers only click categoryname they can see on the webpage, but just in case.
            {
                throw new ArgumentException("There is no matching category in the database.");
            }

            #endregion

            return _salesContext.StockItems
                            .Where(x => x.RemoveFromViewFlag == false &&
                                        x.Discontinued == false &&
                                        x.CategoryID == categoryID)
                            .Select(x => new StockItemView
                            {
                                StockItemID = x.StockItemID,
                                Description = x.Description,
                                SellingPrice = x.SellingPrice

                            }).ToList();

        }





        public List<ShoppingItemsView> AddToShoppingLists(int productID, int qty)
        {

            if (productID == null)
            {
                throw new ArgumentNullException("Product ID should be supplied");
            }

            if (productID <= 0)
            {
                throw new ArgumentException("Product ID should be greater than 0");
            }

            if (qty <= 0)
            {
                throw new ArgumentNullException("Quantity should be greater than 0");
            }

            var productInDatabase = _salesContext.StockItems.Where(x => x.StockItemID == productID).FirstOrDefault();


            if (productInDatabase == null)
            {
                throw new ArgumentException("There is no such item in the database.");
            }

            if (qty > productInDatabase.QuantityOnHand)
            {
                throw new ArgumentException($"You can't order more than quantity on hand. Quantity on hand is {productInDatabase.QuantityOnHand}");
            }

            if (productInDatabase.QuantityOnHand <= 0)
            {
                throw new ArgumentException("We don't have quantity on hand for this item now.");
            }



            var existingItems = shoppingListItems.Where(x => x.ProductID == productID).FirstOrDefault();


            if (existingItems != null)
            {
                if (existingItems.Quantity + qty > productInDatabase.QuantityOnHand)
                {
                    throw new ArgumentException("You can't order more than the quantity on hand.");
                }

                Console.WriteLine("Duplicate item is already in the cart! quantity added to the existing Item");
                existingItems.Quantity += qty;

            }
            else
            {
                shoppingListItems.Add(new ShoppingItemsView
                {
                    ProductID = productID,
                    ProductName = productInDatabase.Description,
                    //Quantity = productInDatabase.QuantityOnHand > 0 ? 1 : throw new ArgumentException("We don't have this item in the inventory"),
                    Quantity = qty,
                    UnitPrice = productInDatabase.SellingPrice,
                });
            }
            return shoppingListItems;

        }



        //it is a method with the same name as above but with a different parameter.so it is considered a different method.
        public List<ShoppingItemsView> AddToShoppingLists(int productID, int qty, List<ShoppingItemsView> notEmptyCart)
        {

            if (productID == null)
            {
                throw new ArgumentNullException("Product ID should be supplied");
            }

            if (productID <= 0)
            {
                throw new ArgumentException("Product ID should be greater than 0");
            }

            if (qty <= 0)
            {
                throw new ArgumentNullException("Quantity should be greater than 0");
            }

            var productInDatabase = _salesContext.StockItems.Where(x => x.StockItemID == productID).FirstOrDefault();


            if (productInDatabase == null)
            {
                throw new ArgumentException("There is no such item in the database.");
            }

            if (qty > productInDatabase.QuantityOnHand)
            {
                throw new ArgumentException($"You can't order more than quantity on hand. Quantity on hand is {productInDatabase.QuantityOnHand}");
            }

            if (productInDatabase.QuantityOnHand <= 0)
            {
                throw new ArgumentException("We don't have quantity on hand for this item now.");
            }


            //using different cart variable//
            var existingItems = notEmptyCart.Where(x => x.ProductID == productID).FirstOrDefault();


            if (existingItems != null)
            {
                if (existingItems.Quantity + qty > productInDatabase.QuantityOnHand)
                {
                    throw new ArgumentException("You can't order more than the quantity on hand.");
                }

                Console.WriteLine("Duplicate item is already in the cart! quantity added to the existing Item");
                existingItems.Quantity += qty;

            }
            else
            {
                notEmptyCart.Add(new ShoppingItemsView
                {
                    ProductID = productID,
                    ProductName = productInDatabase.Description,
                    //Quantity = productInDatabase.QuantityOnHand > 0 ? 1 : throw new ArgumentException("We don't have this item in the inventory"),
                    Quantity = qty,
                    UnitPrice = productInDatabase.SellingPrice,
                });
            }
            return notEmptyCart;

        }


        public List<StockItemView> GetStockItems()
        {

            //get all stockitems for checking qty on hand


            return _salesContext.StockItems
                .Where(x => x.RemoveFromViewFlag == false && x.Discontinued == false)
                .Select(x => new StockItemView
                {
                    StockItemID = x.StockItemID,
                    Description = x.Description,
                    SellingPrice = x.SellingPrice,

                    //for checking qty on hand
                    QuantityOnHand = x.QuantityOnHand

                })
                            .ToList();

        }





        #endregion

    }
}
