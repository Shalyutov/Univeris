using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univeris.Actual;
using Univeris.Global;
using Univeris.Identity.Claims;

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
    internal class Claim : Claim<object>
    {
        private object _value;
        public override object Value => _value;

        public Claim(ClaimType type, User user, object value) : base(type, user, value)
        {
            _value = value;
        }
        public Claim(AccessClaim claim) : base(claim.Type, claim.User, claim.Value)
        {
            _value = claim.Value;
        }
        public Claim(DepartmentClaim claim) : base(claim.Type, claim.User, claim.Value)
        {
            _value = claim.Value;
        }
        public Claim(GroupClaim claim) : base(claim.Type, claim.User, claim.Value)
        {
            _value = claim.Value;
        }
        public static implicit operator DepartmentClaim?(Claim claim)
        {
            if (claim.Value == null) throw new ArgumentNullException();
            if (claim.Value.GetType() == typeof(Department))
                return new DepartmentClaim(claim.Type, claim.User, (Department)claim.Value);
            else
                return null;
        }
        public static implicit operator GroupClaim?(Claim claim)
        {
            if (claim.Value == null) throw new ArgumentNullException();
            if (claim.Value.GetType() == typeof(AcademicGroup))
                return new GroupClaim(claim.Type, claim.User, (Group)claim.Value);
            else
                return null;
        }
        public override string ToString()
        {
            if(this==null) 
                return string.Empty;
            if (_value.GetType() == typeof(Department)) 
                return ((DepartmentClaim)this).ToString();
            else if (_value.GetType() == typeof(AcademicGroup)) 
                return ((GroupClaim)this).ToString();
            else 
                return string.Empty;
        }

    }
}
