using SalesSystem.DAL;
using SalesSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesSystem.ViewModels;

namespace SalesSystem.BLL
{
    public class EmployeeService
    {

        private readonly SalesContext _salesContext;

        internal EmployeeService(SalesContext salesContext)
        {
          
            _salesContext = salesContext;
        }



    }
}
