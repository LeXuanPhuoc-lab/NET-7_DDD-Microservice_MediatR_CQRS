using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ReservationStatus
{
    public int ReservationStatusId { get; set; }

    public string ReservationStatusDesc { get; set; } = null!;

    public virtual ICollection<CoursePackageReservation> CoursePackageReservations { get; set; } = new List<CoursePackageReservation>();
}
