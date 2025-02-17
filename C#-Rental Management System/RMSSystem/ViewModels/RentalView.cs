// ***********************************************************************
// Assembly         : 
// Author           : James Thompson
// Created          : 10-22-2024
//
// Last Modified By : James Thompson
// Last Modified On : 10-22-2024
// ***********************************************************************
// <copyright file="RentalAgreementView.cs" company="NAIT">
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
    /// Class RentalAgreementView.
    /// </summary>
    public class RentalView
    {
        /// <summary>
        /// Gets or sets the rental identifier.
        /// </summary>
        /// <value>The rental identifier.</value>
        public int RentalID { get; set; }
        /// <summary>
        /// Gets or sets the address identifier.
        /// </summary>
        /// <value>The address identifier.</value>
        public int AddressID { get; set; }

        /// <summary>
        /// Gets or sets the rental type identifier.
        /// </summary>
        /// <value>The rental type identifier.</value>
        public int? RentalTypeID { get; set; } = 0;
        /// <summary>
        /// Gets or sets the monthly rent.
        /// </summary>
        /// <value>The monthly rent.</value>
        public decimal MonthlyRent { get; set; }
        /// <summary>
        /// Gets or sets the vacancies.
        /// </summary>
        /// <value>The vacancies.</value>
        public int Vacancies { get; set; } 
        /// <summary>
        /// Gets or sets the maximum vacancy.
        /// </summary>
        /// <value>The maximum vacancy.</value>
        public int MaxVacancy { get; set; }
        /// <summary>
        /// Gets or sets the damage deposit.
        /// </summary>
        /// <value>The damage deposit.</value>
        public decimal? DamageDeposit { get; set; } = 0;
        /// <summary>
        /// Gets or sets the available date.
        /// </summary>
        /// <value>The available date.</value>
        public DateTime? AvailableDate { get; set; }
        public bool RemoveFromViewFlag { get; set; }

        public AddressView Address { get; set; } = new();
        public List<RenterView> Renters { get; set; } = new();

     
    }
}
