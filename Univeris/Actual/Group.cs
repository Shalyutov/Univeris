using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Univeris.Global;

namespace Univeris.Actual
{
    internal class Group : IEquatable<Group?>
    {
        public virtual string Name { get; set; }
        public string Description { get; set; }

        public Group(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Group);
        }

        public bool Equals(Group? other)
        {
            return other is not null &&
                   Name == other.Name &&
                   Description == other.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description);
        }

        public static bool operator ==(Group? left, Group? right)
        {
            return EqualityComparer<Group>.Default.Equals(left, right);
        }

        public static bool operator !=(Group? left, Group? right)
        {
            return !(left == right);
        }
    }
    internal class AcademicGroup : Group, IEquatable<AcademicGroup?>
    {
        public Degree Degree { get; set; }
        public int Year { get; set; }

        public AcademicGroup(string name, string description, Degree degree, int year) : base(name, description)
        {
            Degree = degree ?? throw new ArgumentNullException(nameof(degree));
            Year = year;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as AcademicGroup);
        }

        public bool Equals(AcademicGroup? other)
        {
            return other is not null &&
                   Name == other.Name &&
                   Description == other.Description &&
                   EqualityComparer<Degree>.Default.Equals(Degree, other.Degree) &&
                   Year == other.Year;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Degree, Year);
        }

        public static bool operator ==(AcademicGroup? left, AcademicGroup? right)
        {
            return EqualityComparer<AcademicGroup>.Default.Equals(left, right);
        }

        public static bool operator !=(AcademicGroup? left, AcademicGroup? right)
        {
            return !(left == right);
        }
    }
    internal class CourseGroup : Group, IEquatable<CourseGroup?>
    {
        public Course Course { get; set; }

        public CourseGroup(string name, string description, Course course) : base(name, description)
        {
            Course = course ?? throw new ArgumentNullException(nameof(course));
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as CourseGroup);
        }

        public bool Equals(CourseGroup? other)
        {
            return other is not null &&
                   Name == other.Name &&
                   Description == other.Description &&
                   EqualityComparer<Course>.Default.Equals(Course, other.Course);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Course);
        }

        public static bool operator ==(CourseGroup? left, CourseGroup? right)
        {
            return EqualityComparer<CourseGroup>.Default.Equals(left, right);
        }

        public static bool operator !=(CourseGroup? left, CourseGroup? right)
        {
            return !(left == right);
        }
    }
}
