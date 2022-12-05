using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univeris.Actual;
using Univeris.Global;

namespace Univeris.Identity.Claims
{
    internal class GroupClaim : Claim<Group>
    {
        private Group group;
        public override Group Value => group;

        public GroupClaim(ClaimType type, User user, Group value) : base(type, user, value)
        {
            group = value;
        }
        public static implicit operator Claim(GroupClaim claim)
        {
            if (claim.Value == null) throw new ArgumentNullException();
            return new Claim(claim.Type, claim.User, claim.Value);
        }
        public override string ToString()
        {
            return $"{Type.Name} {group.Name}";
        }
    }
}
