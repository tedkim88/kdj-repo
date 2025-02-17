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
            //Initialize the _salesContext field with the provided HogWildContext instance
            _salesContext = salesContext;
        }


        //    private EmployeeView GetEmployee(int employeeID)
        //    {

        //        return _salesContext.Employees
        //                                    .Where(x => x.EmployeeID == employeeID)
        //                                    .FirstOrDefault();
        //    }

    }
}
