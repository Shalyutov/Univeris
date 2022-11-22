using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris.Identity.Claims
{
    internal class AccessClaim : Claim
    {
        public int Level { get; set; }

        public AccessClaim(ClaimType type, User user, object value, int level) : base(type, user, value)
        {
            Level = level;
        }
    }
}
