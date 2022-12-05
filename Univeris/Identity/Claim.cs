using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris.Identity
{
    internal abstract class Claim<T>
    {
        public ClaimType Type { get; set; }
        public User User { get; set; }
        public virtual T Value { get; private set; }

        public Claim(ClaimType type, User user, T value)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            User = user ?? throw new ArgumentNullException(nameof(user));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
