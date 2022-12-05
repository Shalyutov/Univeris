using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univeris.Actual;

namespace Univeris.Identity.Claims
{
    enum AccessLevel
    {
        Guest, Student, Teacher, Administrator
    }
    internal class AccessClaim : Claim<Course>
    {
        private Course course;
        public AccessLevel Level { get; set; }
        public override Course Value => course;

        public AccessClaim(ClaimType type, User user, Course value, AccessLevel level) : base(type, user, value)
        {
            Level = level;
            course = value;
        }
    }
}
