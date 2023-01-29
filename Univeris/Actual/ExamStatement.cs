using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univeris.Global;
using Univeris.Identity;

namespace Univeris.Actual
{
    public class ExamStatement
    {
        public Course Course { get; set; }
        public Exam Exam { get; set; }
        public int Value { get; set; }
        public User User { get; set; }

        public ExamStatement(Course course, Exam exam, int value, User user)
        {
            Course = course ?? throw new ArgumentNullException(nameof(course));
            Exam = exam ?? throw new ArgumentNullException(nameof(exam));
            Value = value;
            User = user ?? throw new ArgumentNullException(nameof(user));
        }
    }
}
