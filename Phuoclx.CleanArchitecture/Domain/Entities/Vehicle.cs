using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public string VehicleName { get; set; } = null!;

    public string VehicleLicensePlate { get; set; } = null!;

    public DateTime? RegisterDate { get; set; }

    public int? VehicleTypeId { get; set; }

    public string? VehicleImage { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<TeachingSchedule> TeachingSchedules { get; set; } = new List<TeachingSchedule>();

    public virtual VehicleType? VehicleType { get; set; }
}
