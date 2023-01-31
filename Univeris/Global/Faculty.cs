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
    public class Faculty : IEquatable<Faculty?>
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

        public override bool Equals(object? obj)
        {
            return Equals(obj as Faculty);
        }

        public bool Equals(Faculty? other)
        {
            return other is not null &&
                   Name == other.Name &&
                   Description == other.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description);
        }

        public static bool operator ==(Faculty? left, Faculty? right)
        {
            return EqualityComparer<Faculty>.Default.Equals(left, right);
        }

        public static bool operator !=(Faculty? left, Faculty? right)
        {
            return !(left == right);
        }
    }
}
