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
    /// <summary>
    /// Class AddressView.
    /// </summary>
    public class AddressView
    {
        /// <summary>
        /// Gets or sets the address identifier.
        /// </summary>
        /// <value>The address identifier.</value>
        public int AddressID { get; set; }
        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public string Number { get; set; }
        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        /// <value>The street.</value>
        public string Street { get; set; }
        /// <summary>
        /// Gets or sets the community.
        /// </summary>
        /// <value>The community.</value>
        public string Community { get; set; }
        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>The unit.</value>
        public string Unit { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the state of the province.
        /// </summary>
        /// <value>The state of the province.</value>
        public string ProvinceState { get; set; }
        /// <summary>
        /// Gets or sets the postal code zip.
        /// </summary>
        /// <value>The postal code zip.</value>
        public string PostalCodeZip { get; set; }
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string CountryCode { get; set; }
        /// <summary>
        /// Gets or sets the land lord identifier.
        /// </summary>
        /// <value>The land lord identifier.</value>
        public int? LandLordID { get; set; }

        public bool RemoveFromViewFlag { get; set; }

        /// <summary>
        /// Gets or sets the land lord.
        /// </summary>
        /// <value>The land lord.</value>
        public string LandLord { get; set; }
    }
}
