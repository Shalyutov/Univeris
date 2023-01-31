using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univeris.Actual;
using Univeris.Global;

namespace Univeris.Identity.Claims
{
    public class DepartmentClaim : Claim<Department>
    {
        private Department department;
        public override Department Value => department;

        public DepartmentClaim(ClaimType type, User user, Department value) : base(type, user, value)
        {
            this.department = value;
        }
        public static implicit operator Claim(DepartmentClaim? claim)
        {
            if (claim?.Value == null) throw new ArgumentNullException();
            return new Claim(claim.Type, claim.User, claim.Value);
        }
        public override string ToString()
        {
            return $"{Type.Name} \"{department.Name}\"";
        }
         
    }
}
