using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VehicleType
{
    public int VehicleTypeId { get; set; }

    public int? LicenseTypeId { get; set; }

    public string? VehicleTypeDesc { get; set; }

    public double? Cost { get; set; }

    public virtual LicenseType? LicenseType { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
