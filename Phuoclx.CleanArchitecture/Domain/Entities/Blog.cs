using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Blog
{
    public int BlogId { get; set; }

    public string? StaffId { get; set; }

    public string? Content { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public string? Title { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Staff? Staff { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
