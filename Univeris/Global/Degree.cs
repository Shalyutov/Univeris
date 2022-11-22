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

        public Degree(string code, string name, Department department, string description)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Department = department ?? throw new ArgumentNullException(nameof(department));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
