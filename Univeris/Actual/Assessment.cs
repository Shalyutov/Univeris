using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univeris.Global;
using Univeris.Identity;

namespace Univeris.Actual
{
    internal class Assessment
    {
        public Course Course { get; set; }
        public Assignment Assignment { get; set; }
        public int Value { get; set; }
        public User User { get; set; }

        public Assessment(Course course, Assignment assignment, int value, User user)
        {
            Course = course ?? throw new ArgumentNullException(nameof(course));
            Assignment = assignment ?? throw new ArgumentNullException(nameof(assignment));
            Value = value;
            User = user ?? throw new ArgumentNullException(nameof(user));
        }
    }
}
