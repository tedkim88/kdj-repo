﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POSystem.Entities;

[Table("Postion")]
internal partial class Position
{
    [Key]
    public int PositionID { get; set; }

    [Required]
    [StringLength(50)]
    public string Description { get; set; }

    public bool RemoveFromViewFlag { get; set; }

    [InverseProperty("Position")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}