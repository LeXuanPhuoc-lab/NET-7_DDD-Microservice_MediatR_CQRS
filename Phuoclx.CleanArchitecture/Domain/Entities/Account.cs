﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Account
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
