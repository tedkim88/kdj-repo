﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RMSSystem.Entities;

[Index("EmployeeID", Name = "IX_EmployeeID")]
[Index("OfferingID", Name = "IX_OfferingID")]
internal partial class ClassOffering
{
    [Key]
    public int ClassOfferingID { get; set; }

    public int OfferingID { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string SectionCode { get; set; }

    public int EmployeeID { get; set; }

    public string RoomNumber { get; set; }

    public bool Cancelled { get; set; }

    public bool RemoveFromViewFlag { get; set; }

    [InverseProperty("OfferingSection")]
    public virtual ICollection<ClassMember> ClassMembers { get; set; } = new List<ClassMember>();

    [ForeignKey("EmployeeID")]
    [InverseProperty("ClassOfferings")]
    public virtual Employee Employee { get; set; }

    [ForeignKey("OfferingID")]
    [InverseProperty("ClassOfferings")]
    public virtual Offering Offering { get; set; }
}