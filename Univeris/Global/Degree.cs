using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris.Global
{
    /// <summary>
    /// Направление обучения (программа обучения)
    /// </summary>
    internal class Degree
    {
        /// <summary>
        /// Код/классификатор программы обучения
        /// </summary>
        public string Code { get; set; } = "";
        /// <summary>
        /// Наименование программы обучения
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// Выпускающая кафедра
        /// </summary>
        public Department Department { get; set; }
        /// <summary>
        /// Краткое описание
        /// </summary>
        public string Description { get; set; } = "";
    }
}
