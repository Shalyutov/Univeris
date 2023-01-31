using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris.Identity
{
    public class User : IEquatable<User?>
    {
        public int Uuid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public User(int Uuid, string Username, string Email, string Phone, string Password)
        {
            this.Uuid = Uuid;
            this.Username = Username ?? throw new ArgumentNullException(nameof(Username));
            this.Email = Email ?? throw new ArgumentNullException(nameof(Email));
            this.Phone = Phone ?? throw new ArgumentNullException(nameof(Phone));
            this.Password = Password ?? throw new ArgumentNullException(nameof(Password));
        }
        
        public bool IsPasswordValid(string password) => password == Password; 

        public override bool Equals(object? obj)
        {
            return Equals(obj as User);
        }

        public bool Equals(User? other)
        {
            return other is not null &&
                   Uuid == other.Uuid &&
                   Username == other.Username &&
                   Email == other.Email &&
                   Phone == other.Phone && 
                   Password == other.Password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Uuid, Username, Email, Phone, Password);
        }

        public static bool operator ==(User? left, User? right)
        {
            return EqualityComparer<User>.Default.Equals(left, right);
        }

        public static bool operator !=(User? left, User? right)
        {
            return !(left == right);
        }
    }
}
