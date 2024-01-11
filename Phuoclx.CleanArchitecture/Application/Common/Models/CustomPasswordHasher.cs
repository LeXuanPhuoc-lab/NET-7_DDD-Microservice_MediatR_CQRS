namespace Application.Common.Models
{
    public class CustomPasswordHasher : IPasswordHasher<ApplicationUser>
    {
        public string HashPassword(ApplicationUser user, string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 13);// 13: given cost for particular iteration provided
        }

        public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
        {
            var isMatch = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
            if (!isMatch) return PasswordVerificationResult.Failed;
            return PasswordVerificationResult.Success;
        }
    }
}
