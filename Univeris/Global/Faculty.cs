using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris.Global
{
    internal class Faculty
    {
        /// <summary>
        /// Наименование института/высшей школы/факультета
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// Краткое описание или ссылка на страницу (markdown)
        /// </summary>
        public string Description { get; set; } = "";
    }
}
