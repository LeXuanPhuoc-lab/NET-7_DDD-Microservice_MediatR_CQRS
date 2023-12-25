using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class QuestionAnswer
{
    public int QuestionAnswerId { get; set; }

    public string? Answer { get; set; }

    public bool? IsTrue { get; set; }

    public int? QuestionId { get; set; }

    public virtual Question? Question { get; set; }
}
