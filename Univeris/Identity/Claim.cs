using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class Claim<T> : IEquatable<Claim<T>?>
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

        public override bool Equals(object? obj)
        {
            return Equals(obj as Claim<T>);
        }

        public bool Equals(Claim<T>? other)
        {
            return other is not null &&
                   EqualityComparer<ClaimType>.Default.Equals(Type, other.Type) &&
                   EqualityComparer<User>.Default.Equals(User, other.User) &&
                   EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, User, Value);
        }

        public static bool operator ==(Claim<T>? left, Claim<T>? right)
        {
            return EqualityComparer<Claim<T>>.Default.Equals(left, right);
        }

        public static bool operator !=(Claim<T>? left, Claim<T>? right)
        {
            return !(left == right);
        }
        
    }
    
    public class Claim : Claim<object>
    {
        private object _value;
        public override object Value => _value;

        [JsonConstructor]
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
            if (claim.Value is Department)
                return new DepartmentClaim(claim.Type, claim.User, (Department)claim.Value);
            else
                return null;
        }
        public static implicit operator GroupClaim?(Claim claim)
        {
            if (claim.Value == null) throw new ArgumentNullException();
            if (claim.Value is AcademicGroup)
                return new GroupClaim(claim.Type, claim.User, (Group)claim.Value);
            else
                return null;
        }
        public override string ToString()
        {
            if (this == null)
                return string.Empty;

            return Value switch
            {
                Department => ((DepartmentClaim)this).ToString(),
                Group => ((GroupClaim)this).ToString(),
                _ => string.Empty,
            };
        }
    }
}
