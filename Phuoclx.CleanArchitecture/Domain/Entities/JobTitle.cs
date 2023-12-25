using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class JobTitle
{
    public int JobTitleId { get; set; }

    public string JobTitleDesc { get; set; } = null!;

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
