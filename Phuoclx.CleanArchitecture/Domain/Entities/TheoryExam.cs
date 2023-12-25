using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TheoryExam
{
    public int TheoryExamId { get; set; }

    public int? TotalQuestion { get; set; }

    public int? TotalTime { get; set; }

    public int? TotalAnswerRequired { get; set; }

    public int? LicenseTypeId { get; set; }

    public DateTime? StartDate { get; set; }

    public TimeSpan? StartTime { get; set; }

    public bool? IsMockExam { get; set; }

    public virtual ICollection<ExamGrade> ExamGrades { get; set; } = new List<ExamGrade>();

    public virtual ICollection<ExamHistory> ExamHistories { get; set; } = new List<ExamHistory>();

    public virtual LicenseType? LicenseType { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
