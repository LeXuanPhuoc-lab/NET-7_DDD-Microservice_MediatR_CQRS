using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ExamGrade
{
    public int ExamGradeId { get; set; }

    public string? MemberId { get; set; }

    public int? TheoryExamId { get; set; }

    public double? Point { get; set; }

    public int QuestionId { get; set; }

    public int SelectedAnswerId { get; set; }

    public string? Email { get; set; }

    public DateTime? StartDate { get; set; }

    public virtual Member? Member { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual TheoryExam? TheoryExam { get; set; }
}
