using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris.Identity
{
    internal class User : IEquatable<User?>
    {
        private int uuid;
        private string name;
        private string password;
        private string email;
        private string phone;
        public int Uuid { get => uuid; private set => uuid = value; }
        public string Username { get => name; private set => name = value; }
        public string Email { get => email; private set => email = value; }
        public string Phone { get => phone; private set => phone = value; }
        public string Password { private get => password; private set => password = value; }

        public User(int uuid, string username, string password, string email, string phone)
        {
            Uuid = uuid;
            this.name = username ?? throw new ArgumentNullException(nameof(name));
            this.password = password ?? throw new ArgumentNullException(nameof(password));
            this.email = email ?? throw new ArgumentNullException(nameof(email));
            this.phone = phone ?? throw new ArgumentNullException(nameof(phone));
        }
        
        public bool IsPasswordValid(string password) => password == this.password; 

        public override bool Equals(object? obj)
        {
            return Equals(obj as User);
        }

        public bool Equals(User? other)
        {
            return other is not null &&
                   Uuid == other.Uuid &&
                   name == other.name &&
                   password == other.password &&
                   email == other.email &&
                   phone == other.phone;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Uuid, name, password, email, phone);
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
