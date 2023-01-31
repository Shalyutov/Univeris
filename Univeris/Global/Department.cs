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
    public class Department : IEquatable<Department?>
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

        public Department(string name, Faculty faculty, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Faculty = faculty ?? throw new ArgumentNullException(nameof(faculty));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Department);
        }

        public bool Equals(Department? other)
        {
            return other is not null &&
                   Name == other.Name &&
                   EqualityComparer<Faculty>.Default.Equals(Faculty, other.Faculty) &&
                   Description == other.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Faculty, Description);
        }

        public static bool operator ==(Department? left, Department? right)
        {
            return EqualityComparer<Department>.Default.Equals(left, right);
        }

        public static bool operator !=(Department? left, Department? right)
        {
            return !(left == right);
        }
    }
}
