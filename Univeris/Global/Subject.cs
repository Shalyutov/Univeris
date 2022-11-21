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
        /// <summary>
        /// Название дисциплины
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// Образовательная программа
        /// </summary>
        public Degree Degree { get; set; }
        /// <summary>
        /// Краткое описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Констурктор дисциплины
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="degree">Направление</param>
        /// <param name="description">Описание</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Subject(string name, Degree degree, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Degree = degree ?? throw new ArgumentNullException(nameof(degree));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
