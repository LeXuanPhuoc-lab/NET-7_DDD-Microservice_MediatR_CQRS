using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Slot
{
    public int SlotId { get; set; }

    public string? SlotName { get; set; }

    public int? Duration { get; set; }

    public TimeSpan? Time { get; set; }

    public string? SlotDesc { get; set; }

    public virtual ICollection<TeachingSchedule> TeachingSchedules { get; set; } = new List<TeachingSchedule>();
}
