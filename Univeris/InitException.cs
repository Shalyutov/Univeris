using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris
{
    public class InitException : Exception
    {
        public InitException(string message) : base($"Ошибка инициализации данных - {message}") { }
    }
}
