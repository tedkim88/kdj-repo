﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RMSSystem.Entities;

internal partial class Course
{
    [Key]
    [StringLength(7)]
    [Unicode(false)]
    public string CourseID { get; set; }

    [Required]
    [StringLength(75)]
    [Unicode(false)]
    public string CourseName { get; set; }

    [Column(TypeName = "decimal(3, 1)")]
    public decimal Credits { get; set; }

    public int? TotalHours { get; set; }

    public int? ClassroomType { get; set; }

    public int Term { get; set; }

    [Column(TypeName = "money")]
    public decimal Tuition { get; set; }

    [StringLength(1028)]
    [Unicode(false)]
    public string Description { get; set; }

    public bool RemoveFromViewFlag { get; set; }

    [InverseProperty("Course")]
    public virtual ICollection<PlannedAssessment> PlannedAssessments { get; set; } = new List<PlannedAssessment>();

    [InverseProperty("Course")]
    public virtual ICollection<ProgramCourse> ProgramCourses { get; set; } = new List<ProgramCourse>();
}