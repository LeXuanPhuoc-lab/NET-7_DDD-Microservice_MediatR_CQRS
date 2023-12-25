using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Staff
{
    public string StaffId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateBirth { get; set; }

    public string Phone { get; set; } = null!;

    public bool? IsActive { get; set; }

    public string? AvatarImage { get; set; }

    public string? Email { get; set; }

    public string? AddressId { get; set; }

    public int? JobTitleId { get; set; }

    public string? SelfDescription { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<CoursePackageReservation> CoursePackageReservations { get; set; } = new List<CoursePackageReservation>();

    public virtual Account? EmailNavigation { get; set; }

    public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

    public virtual JobTitle? JobTitle { get; set; }

    public virtual ICollection<TeachingSchedule> TeachingSchedules { get; set; } = new List<TeachingSchedule>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
