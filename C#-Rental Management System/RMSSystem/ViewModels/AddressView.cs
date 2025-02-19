// ***********************************************************************
// Assembly         : 
// Author           : James Thompson
// Created          : 10-22-2024
//
// Last Modified By : James Thompson
// Last Modified On : 10-22-2024
// ***********************************************************************
// <copyright file="AddressView.cs" company="NAIT">
//     Copyright (c) NAIT. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSystem.ViewModels
{
   
    public class AddressView
    {
    
        public int AddressID { get; set; }
   
        public string Number { get; set; }
   
        public string Street { get; set; }
       
        public string Community { get; set; }
      
        public string Unit { get; set; }
    
        public string City { get; set; }
   
        public string ProvinceState { get; set; }
       
        public string PostalCodeZip { get; set; }
       
        public string CountryCode { get; set; }
       
        public int? LandLordID { get; set; }

        public bool RemoveFromViewFlag { get; set; }

       
        public string LandLord { get; set; }
    }
}
