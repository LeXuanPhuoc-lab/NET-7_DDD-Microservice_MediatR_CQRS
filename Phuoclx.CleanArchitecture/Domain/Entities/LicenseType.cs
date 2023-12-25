using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class LicenseType
{
    public int LicenseTypeId { get; set; }

    public string LicenseTypeDesc { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<LicenseRegisterForm> LicenseRegisterForms { get; set; } = new List<LicenseRegisterForm>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<SimulationSituation> SimulationSituations { get; set; } = new List<SimulationSituation>();

    public virtual ICollection<TheoryExam> TheoryExams { get; set; } = new List<TheoryExam>();

    public virtual ICollection<VehicleType> VehicleTypes { get; set; } = new List<VehicleType>();
}
