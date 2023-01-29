using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univeris.Global;

namespace Univeris.Actual
{
    public class Course : IEquatable<Course?>
    {
        public Subject Subject { get; set; }
        public int Year { get; set; }

        public Course(Subject subject, int year)
        {
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            Year = year;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Course);
        }

        public bool Equals(Course? other)
        {
            return other is not null &&
                   EqualityComparer<Subject>.Default.Equals(Subject, other.Subject) &&
                   Year == other.Year;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Subject, Year);
        }

        public static bool operator ==(Course? left, Course? right)
        {
            return EqualityComparer<Course>.Default.Equals(left, right);
        }

        public static bool operator !=(Course? left, Course? right)
        {
            return !(left == right);
        }
    }
}
