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
    public class Core
    {
        public event OnCoreInitialized? CoreInitialized;
        public event OnSelect? DataSelected;
        public event OnUpdate? DataUpdated;
        public event OnCompute? ValueComputed;
        public event OnSignedIn? UserSignedIn;
        public event OnError? OnErrorOccured;
        public event OnCoreAction? CoreAction;

        public User? User;
        public Data Data;

        public Core()
        {
            Data = new Data();
        }
        public void Start(bool TemplateSession)
        {
            CoreInitialized?.Invoke();
            if (TemplateSession)
            {
                Data.GetTemplate();
                CoreAction?.Invoke("Загружен шаблон конфиграции");
                return;
            }
            try
            {
                Data.Open();
                CoreAction?.Invoke("Открыт файл конфигурации");
            }
            catch (FileNotFoundException)
            {
                CoreAction?.Invoke("Файла конфигурации не существует");
                Data.GetTemplate();
                CoreAction?.Invoke("Установлен шаблон конфиграции");
                Data.Save();
            }
            catch (Exception)
            {
                OnErrorOccured?.Invoke("Ошибка чтения файла конфигурации");
            }
        }

        #region Identity Access Management
        public User? FindUser(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;
            User? user = Data.Context.Users.Find(user => user.Username == username);
            DataSelected?.Invoke("Произведён поиск пользователя с указанным именем");
            return user;
        }
        public bool SignIn(string username, string password)
        {
            User? user = FindUser(username);
            if (user == null) 
            { 
                return false; 
            }
            else if(user.IsPasswordValid(password))
            {
                this.User = user;
                UserSignedIn?.Invoke($"Пользователь {user.Username} вошёл в система");
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
        public bool IsAccessValid(User user, object subject)//TODO
        {
            return true;
        }
        #endregion

        #region Point Rating System
        public List<Course> GetCourses(User user)
        {
            List<Course> courses = new();
            var accessClaims = Data.Context.CourseAccess.FindAll(claim => claim.User == user);
            foreach (var claim in accessClaims) courses.Add(claim.Value);
            DataSelected?.Invoke("Выбраны все курсы для указанного пользователя");
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
            DataSelected?.Invoke("Выбраны все подходящие по указанной роли курсы для указанного пользователя");
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
            DataSelected?.Invoke("Получены все оценки текущего контроля для пользователя в рамках курса");
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
            DataSelected?.Invoke("Запрошен результат экзамена для пользователя в рамках курса");
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
        public List<Assignment> GetAssignments(Subject subject)
        {
            List<Assignment> assignments;
            assignments = Data.Context.Assignments.FindAll(assignment => assignment.Subject == subject);
            DataSelected?.Invoke("Получены все контрольные мероприятия в рамках дисциплины");
            return assignments;
        }
        public float GetRating(List<Assignment> assignments, List<Assessment> assessments, ExamStatement? statement, bool CurrentOnly)
        {
            float rating = 0.0f;
            float sum = 0.0f;
            foreach (var assignment in assignments) 
                sum += assignment.Score;
            foreach (var assessment in assessments) 
                rating += (assessment.Value * 1.0f / assessment.Assignment.Score * 1.0f) * (assessment.Assignment.Score * 1.0f / sum);
            if(!CurrentOnly)
            {
                rating *= 0.6f;
                rating += (statement!.Value * 1.0f / statement.Exam.Score * 1.0f) * 0.4f;
                ValueComputed?.Invoke("Подсчитан рейтинг на основе текущего рейтинга с учётом сдачи экзамена");
            }
            else
            {
                ValueComputed?.Invoke("Подсчитан рейтинг на основе текущего рейтинга");
            }
            return rating * 100.0f;
        }
        public float GetRating(List<Assignment> assignments, List<Assessment> assessments)
        {
            float rating;
            rating = GetRating(assignments, assessments, null, true);
            return rating;
        }
        public bool SetAssessment(Course course, Assignment assignment, int value, User user)
        {
            Assessment assessment = new Assessment(course, assignment, value, user);
            Data.Context.Assessments.Add(assessment);
            DataUpdated?.Invoke("Зафиксирована оценка за контрольное мероприятие");
            return true;
        }
        public bool SetExamStatement(Course course, Exam exam, int value, User user)
        {
            ExamStatement statement = new ExamStatement(course, exam, value, user);
            Data.Context.ExamStatements.Add(statement);
            DataUpdated?.Invoke("Зафиксирована оценка за аттестационное мероприятие");
            return true;
        }
        #endregion

        public delegate void OnCoreInitialized();
        public delegate void OnSelect(string entity);
        public delegate void OnUpdate(string entity);
        public delegate void OnCompute(string entity);
        public delegate void OnSignedIn(string entity);
        public delegate void OnError(string entity);
        public delegate void OnCoreAction(string entity);
    }
}
