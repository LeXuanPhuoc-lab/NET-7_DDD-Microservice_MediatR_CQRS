using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class LicenseRegisterFormStatus
{
    public int RegisterFormStatusId { get; set; }

    public string? RegisterFormStatusDesc { get; set; }

    public virtual ICollection<LicenseRegisterForm> LicenseRegisterForms { get; set; } = new List<LicenseRegisterForm>();
}
