using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payloads.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; } = null!;
    }
}
