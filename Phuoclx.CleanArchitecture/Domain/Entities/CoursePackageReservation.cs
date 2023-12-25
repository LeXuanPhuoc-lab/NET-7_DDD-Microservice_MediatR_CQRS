using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CoursePackageReservation
{
    public string CoursePackageReservationId { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public string MemberId { get; set; } = null!;

    public string CoursePackageId { get; set; } = null!;

    public string StaffId { get; set; } = null!;

    public int? ReservationStatusId { get; set; }

    public int PaymentTypeId { get; set; }

    public double? PaymentAmmount { get; set; }

    public virtual CoursePackage CoursePackage { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;

    public virtual PaymentType PaymentType { get; set; } = null!;

    public virtual ReservationStatus? ReservationStatus { get; set; }

    public virtual Staff Staff { get; set; } = null!;
}
