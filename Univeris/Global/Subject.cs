using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris.Global
{
    /// <summary>
    /// Дисциплина образовательной программы
    /// </summary>
    internal class Subject
    {
        public string Name { get; set; } = "";
        public Degree Degree { get; set; }
        public string Description { get; set; }

    }
}
