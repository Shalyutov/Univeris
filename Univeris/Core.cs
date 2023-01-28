using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Univeris.Actual;
using Univeris.Global;
using Univeris.Identity;
using Univeris.Identity.Claims;

namespace Univeris
{
    internal class Core
    {
        public User? User;
        public Data Data;
        public Core()
        {
            Data = new Data();
        }
        #region Identity Access Management
        public User? FindUser(Data data, string username)
        {
            if (string.IsNullOrEmpty(username)) return null;
            User? user = data.Context.Users.Find(user => user.Username == username);
            return user;
        }
        public bool SignIn(string username, string password)
        {
            User? user = FindUser(Data, username);
            if (user == null) 
            { 
                return false; 
            }
            else if(user.IsPasswordValid(password))
            {
                this.User = user;
                return true;
            }
            return false;
        }
        public void SignOut()
        {
            if (User != null)
            {
                User = null;
            }
        }
        #endregion
        #region Point Rating System
        public List<Course> GetCourses(User user)
        {
            List<Course> courses = new();
            var accessClaims = Data.Context.CourseAccess.FindAll(claim => claim.User == user);
            foreach (var claim in accessClaims) courses.Add(claim.Value);
            return courses;
        }
        public List<Course> GetCourses()
        {
            if (User != null)
            {
                return GetCourses(User);
            }
            else
            {
                return new List<Course>();
            }
        }
        public List<Course> GetCourses(User user, AccessLevel level)
        {
            List<Course> courses = new();
            var accessClaims = Data.Context.CourseAccess.FindAll(claim => claim.User == user && claim.Level == level);
            foreach (var claim in accessClaims) courses.Add(claim.Value);
            return courses;
        }
        public List<Course> GetCourses(AccessLevel level)
        {
            if (User != null)
            {
                return GetCourses(User, level);
            }
            else
            {
                return new List<Course>();
            }
        }
        public List<Assessment> GetAssessments(User user, Course course)
        {
            List<Assessment> assessments;
            assessments = Data.Context.Assessments.FindAll(assessment => assessment.User == user && assessment.Course == course);
            return assessments;
        }
        public List<Assessment> GetAssessments(Course course)
        {
            if (User != null)
            {
                return GetAssessments(User, course);
            }
            else
            {
                return new List<Assessment>();
            }
        }
        public ExamStatement? GetExamStatement(User user, Course course)
        {
            ExamStatement? statement;
            statement = Data.Context.ExamStatements.Find(exam => exam.User == user && exam.Course == course);
            return statement;
        }
        public ExamStatement? GetExamStatement(Course course)
        {
            if (User != null)
            {
                return GetExamStatement(User, course);
            }
            else
            {
                return null;
            }
        }
        public float GetRating(List<Assessment> assessments, ExamStatement? statement, bool CurrentOnly)
        {
            float rating = 0.0f;
            float sum = 0.0f;
            foreach (var assessment in assessments) sum += assessment.Assignment.Score;
            foreach (var assessment in assessments) rating += (assessment.Value * 1.0f / assessment.Assignment.Score * 1.0f)*(assessment.Assignment.Score*1.0f/sum);
            if(!CurrentOnly)
            {
                rating *= 0.6f;
                rating += (statement!.Value * 1.0f / statement.Exam.Score * 1.0f) * 0.4f;
            }
            return rating * 100.0f;
        }
        public float GetRating(List<Assessment> assessments)
        {
            float rating;
            rating = GetRating(assessments, null, true);
            return rating;
        }
        #endregion
    }
}
