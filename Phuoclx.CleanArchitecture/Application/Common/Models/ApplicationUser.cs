namespace Application.Common.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool Gender { get; set; }
        public DateTime Dob { get; set; }
        public string? IdentityCardNumber { get; set; }
    }
}
