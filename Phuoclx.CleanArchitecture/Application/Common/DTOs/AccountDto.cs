namespace Application.Common.DTOs
{
    public class AccountDto
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int RoleId { get; set; }

        public bool? IsActive { get; set; }

        //public virtual ICollection<Member> Members { get; set; } = new List<Member>();

        public virtual RoleDto Role { get; set; } = null!;

        //public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
    }
}
