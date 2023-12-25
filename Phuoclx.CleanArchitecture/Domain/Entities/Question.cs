using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Question
{
    public int QuestionId { get; set; }

    public string? QuestionAnswerDesc { get; set; }

    public bool? IsParalysis { get; set; }

    public string? Image { get; set; }

    public int? LicenseTypeId { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<ExamGrade> ExamGrades { get; set; } = new List<ExamGrade>();

    public virtual LicenseType? LicenseType { get; set; }

    public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; } = new List<QuestionAnswer>();

    public virtual ICollection<TheoryExam> TheoryExams { get; set; } = new List<TheoryExam>();
}
