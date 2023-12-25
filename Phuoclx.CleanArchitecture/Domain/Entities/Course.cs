using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Course
{
    public string CourseId { get; set; } = null!;

    public string CourseTitle { get; set; } = null!;

    public string? CourseDesc { get; set; }

    public int? TotalMonth { get; set; }

    public DateTime? StartDate { get; set; }

    public bool? IsActive { get; set; }

    public int? LicenseTypeId { get; set; }

    public int? TotalHoursRequired { get; set; }

    public int? TotalKmRequired { get; set; }

    public virtual ICollection<CoursePackage> CoursePackages { get; set; } = new List<CoursePackage>();

    public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

    public virtual LicenseType? LicenseType { get; set; }

    public virtual ICollection<WeekdaySchedule> WeekdaySchedules { get; set; } = new List<WeekdaySchedule>();

    public virtual ICollection<Curriculum> Curricula { get; set; } = new List<Curriculum>();

    public virtual ICollection<Staff> Mentors { get; set; } = new List<Staff>();
}
