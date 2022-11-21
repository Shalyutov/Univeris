using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris.Global
{
    /// <summary>
    /// Кафедра/Подразделение
    /// </summary>
    internal class Department
    {
        /// <summary>
        /// Наименование кафедры/подразделения
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// Управляющий факультет
        /// </summary>
        public Faculty Faculty { get; set; }
        /// <summary>
        /// Краткое описание или ссылка на страницу (markdown)
        /// </summary>
        public string Description { get; set; } = "";
    }
}
