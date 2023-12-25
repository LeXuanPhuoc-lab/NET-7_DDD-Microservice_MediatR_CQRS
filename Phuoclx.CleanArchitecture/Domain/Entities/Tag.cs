using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Tag
{
    public int TagId { get; set; }

    public string? TagName { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
