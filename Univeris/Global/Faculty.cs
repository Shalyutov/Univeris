using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris.Global
{
    /// <summary>
    /// Факультет/институт/высшая школа
    /// </summary>
    public class Faculty
    {
        /// <summary>
        /// Наименование института/высшей школы/факультета
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// Краткое описание или ссылка на страницу (markdown)
        /// </summary>
        public string Description { get; set; } = "";
        /// <summary>
        /// Конструктор факультета
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="description">Описание</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Faculty(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
