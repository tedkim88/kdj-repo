// ***********************************************************************
// Assembly         : 
// Author           : James Thompson
// Created          : 10-22-2024
//
// Last Modified By : James Thompson
// Last Modified On : 10-22-2024
// ***********************************************************************
// <copyright file="RenterView.cs" company="NAIT">
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
    /// Class RenterView.
    /// </summary>
    public class RenterView
    {
        /// <summary>
        /// Gets or sets the renter identifier.
        /// </summary>
        /// <value>The renter identifier.</value>
        public int RenterID { get; set; }
        /// <summary>
        /// Gets or sets the student number.
        /// </summary>
        /// <value>The student number.</value>
        public int StudentNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the student.
        /// </summary>
        /// <value>The name of the student.</value>
        public string StudentName { get; set; }
        /// <summary>
        /// Gets or sets the rental identifier.
        /// </summary>
        /// <value>The rental identifier.</value>
        public int RentalID { get; set; }
        /// <summary>
        /// Gets or sets the effective date.
        /// </summary>
        /// <value>The effective date.</value>
        public DateTime EffectiveDate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RenterView"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active { get; set; }
        /// <summary>
        /// Gets or sets the name of the emergency contact.
        /// </summary>
        /// <value>The name of the emergency contact.</value>
        public string EmergencyContactName { get; set; }
        /// <summary>
        /// Gets or sets the emergency contact number.
        /// </summary>
        /// <value>The emergency contact number.</value>
        public string EmergencyContactNumber { get; set; }
        public bool RemoveFromViewFlag { get; set; }

    }
}
