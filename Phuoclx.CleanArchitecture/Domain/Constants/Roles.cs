using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    public abstract class UserRoles 
    {
        public const string Administrator = nameof(Administrator);
        public const string Manager = nameof(Manager);
        public const string Mentor = nameof(Mentor);
        public const string Member = nameof(Member);
    }
}
