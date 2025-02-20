﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RMSSystem.Entities;

[Keyless]
internal partial class CurrentClassOffering
{
    public int ClassOfferingID { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string ProgramName { get; set; }

    [Required]
    [StringLength(5)]
    [Unicode(false)]
    public string Semester { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string SectionCode { get; set; }

    [Required]
    [StringLength(7)]
    [Unicode(false)]
    public string CourseID { get; set; }
}