using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PaymentType
{
    public int PaymentTypeId { get; set; }

    public string? PaymentTypeDesc { get; set; }

    public virtual ICollection<CoursePackageReservation> CoursePackageReservations { get; set; } = new List<CoursePackageReservation>();
}
