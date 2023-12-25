using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class FeedBack
{
    public int FeedbackId { get; set; }

    public string? Content { get; set; }

    public int? RatingStar { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? MemberId { get; set; }

    public string? StaffId { get; set; }

    public string? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Member? Member { get; set; }

    public virtual Staff? Staff { get; set; }
}
