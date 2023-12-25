using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class SimulationSituation
{
    public int SimulationId { get; set; }

    public string? SimulationVideo { get; set; }

    public string? ImageResult { get; set; }

    public int? TimeResult { get; set; }

    public int? LicenseTypeId { get; set; }

    public virtual LicenseType? LicenseType { get; set; }
}
