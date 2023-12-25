using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Member
{
    public string MemberId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime? DateBirth { get; set; }

    public string? Phone { get; set; }

    public bool? IsActive { get; set; }

    public string? AvatarImage { get; set; }

    public string? AddressId { get; set; }

    public string? Email { get; set; }

    public int? LicenseFormId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<CoursePackageReservation> CoursePackageReservations { get; set; } = new List<CoursePackageReservation>();

    public virtual Account? EmailNavigation { get; set; }

    public virtual ICollection<ExamGrade> ExamGrades { get; set; } = new List<ExamGrade>();

    public virtual ICollection<ExamHistory> ExamHistories { get; set; } = new List<ExamHistory>();

    public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

    public virtual LicenseRegisterForm? LicenseForm { get; set; }

    public virtual ICollection<RollCallBook> RollCallBooks { get; set; } = new List<RollCallBook>();
}
