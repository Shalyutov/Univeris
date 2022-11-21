using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris.Identity
{
    internal class User
    {
        public int Uuid { get; set; }
        private string name;
        private string password;
        private string email;
        private string phone;
    }
}
