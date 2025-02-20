﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RMSSystem.Entities;

[Index("AddressID", Name = "IX_AddressID")]
internal partial class Rental
{
    [Key]
    public int RentalID { get; set; }

    public int AddressID { get; set; }

    public int? RentalTypeID { get; set; }

    /// <summary>
    /// Monthly rent for the entire unit, regardless of the number of renters
    /// </summary>
    [Column(TypeName = "money")]
    public decimal MonthlyRent { get; set; }

    public int MaxVacancy { get; set; }

    [Column(TypeName = "money")]
    public decimal? DamageDeposit { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AvailableDate { get; set; }

    public bool RemoveFromViewFlag { get; set; }

    [ForeignKey("AddressID")]
    [InverseProperty("Rentals")]
    public virtual Address Address { get; set; }

    [InverseProperty("Rental")]
    public virtual ICollection<RentalPayment> RentalPayments { get; set; } = new List<RentalPayment>();

    [ForeignKey("RentalTypeID")]
    [InverseProperty("Rentals")]
    public virtual RentalType RentalType { get; set; }

    [InverseProperty("Rental")]
    public virtual ICollection<Renter> Renters { get; set; } = new List<Renter>();
}