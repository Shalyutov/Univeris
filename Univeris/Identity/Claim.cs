using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris.Identity
{
    internal class Claim
    {
        public ClaimType Type { get; set; }
        public User User { get; set; }
        public object Value { get; set; }

        public Claim(ClaimType type, User user, object value)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            User = user ?? throw new ArgumentNullException(nameof(user));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
