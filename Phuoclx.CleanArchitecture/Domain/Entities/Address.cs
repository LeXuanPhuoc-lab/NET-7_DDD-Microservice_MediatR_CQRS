using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Address
{
    public string AddressId { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string District { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? Zipcode { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
