using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class AppSettings
    {
        public string Key { get; set; } = string.Empty;
        public TimeSpan TokenLifeTime { get; set; }
    }
}
