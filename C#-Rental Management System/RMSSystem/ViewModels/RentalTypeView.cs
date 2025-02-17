// ***********************************************************************
// Assembly         : 
// Author           : James Thompson
// Created          : 10-22-2024
//
// Last Modified By : James Thompson
// Last Modified On : 10-22-2024
// ***********************************************************************
// <copyright file="RentalTypeView.cs" company="NAIT">
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
    /// Class RentalTypeView.
    /// </summary>
    public class RentalTypeView
    {
        /// <summary>
        /// Gets or sets the rental type identifier.
        /// </summary>
        /// <value>The rental type identifier.</value>
        public int RentalTypeID { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        public bool RemoveFromViewFlag { get; set; }
    }
}
