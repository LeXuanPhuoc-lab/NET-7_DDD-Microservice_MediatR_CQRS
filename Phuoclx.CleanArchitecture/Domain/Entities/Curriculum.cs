using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Curriculum
{
    public int CurriculumId { get; set; }

    public string CurriculumDesc { get; set; } = null!;

    public string? CurriculumDetail { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
