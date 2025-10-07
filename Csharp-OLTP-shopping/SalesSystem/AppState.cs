using SalesSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem
{
    public class AppState
    {
        public List<ShoppingItemsView> cartView { get; set; } = new();
    }
}
