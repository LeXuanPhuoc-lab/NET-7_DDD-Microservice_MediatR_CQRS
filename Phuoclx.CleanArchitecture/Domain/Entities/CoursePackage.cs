using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CoursePackage
{
    public string CoursePackageId { get; set; } = null!;

    public string? CoursePackageDesc { get; set; }

    public int? TotalSession { get; set; }

    public int? SessionHour { get; set; }

    public double? Cost { get; set; }

    public int? AgeRequired { get; set; }

    public string? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<CoursePackageReservation> CoursePackageReservations { get; set; } = new List<CoursePackageReservation>();

    public virtual ICollection<TeachingSchedule> TeachingSchedules { get; set; } = new List<TeachingSchedule>();
}
