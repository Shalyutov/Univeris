using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univeris.Actual;
using Univeris.Global;
using Univeris.Identity;
using Univeris.Identity.Claims;

namespace Univeris
{
    internal class Context
    {
        #region Global
        public List<Faculty> Faculties { get; set; } = new List<Faculty>();
        public List<Department> Departments { get; set; } = new List<Department>();
        public List<Degree> Degrees { get; set; } = new List<Degree>();
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public List<Exam> Exams { get; set; } = new List<Exam>();
        public List<ExamType> ExamTypes { get; set; } = new List<ExamType>();
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public List<AssignmentType> AssignmentTypes { get; set; } = new List<AssignmentType>();
        #endregion
        #region Actual
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Assessment> Assessments { get; set; } = new List<Assessment>();
        public List<ExamStatement> ExamStatements { get; set; } = new List<ExamStatement>();
        public List<Group> Groups { get; set; } = new List<Group>();
        #endregion
        #region Identity
        public List<User> Users { get; set; } = new List<User>();
        public List<ClaimType> ClaimTypes { get; set; } = new List<ClaimType>();
        public List<AccessClaim> CourseAccess { get; set; } = new List<AccessClaim>();
        public List<Claim> Claims { get; set; } = new List<Claim>();
        #endregion
        
    }
}
