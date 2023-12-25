using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class LicenseRegisterForm
{
    public int LicenseFormId { get; set; }

    public string? LicenseFormDesc { get; set; }

    public string? Image { get; set; }

    public string? IdentityCardImage { get; set; }

    public string? HealthCertificationImage { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? RegisterFormStatusId { get; set; }

    public string? Gender { get; set; }

    public string? PermanentAddress { get; set; }

    public string? IdentityNumber { get; set; }

    public int? LicenseTypeId { get; set; }

    public DateTime? IdentityCardIssuedDate { get; set; }

    public string? IdentityCardIssuedBy { get; set; }

    public DateTime? LicenseTypeIssuedDate { get; set; }

    public string? AvailableLicenseType { get; set; }

    public virtual LicenseType? LicenseType { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual LicenseRegisterFormStatus? RegisterFormStatus { get; set; }
}
