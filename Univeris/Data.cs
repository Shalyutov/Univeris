using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univeris.Global;
using Univeris.Actual;
using Univeris.Identity;
using Univeris.Identity.Claims;
using Newtonsoft.Json;

namespace Univeris
{
    internal class Data
    {
        public Context Context { get; set; }
        public Data()
        {
            //Open();
            Context = new Context();
            GetFaculty();
            GetStudy();
            GetActualIdentities();
            GetActualStudy();
            GetGroups();
            GetClaims();
            //Save();
        }
        void GetFaculty()
        {
            Context.Faculties.Add(new Faculty("Высшая школа электроники и компьютерных наук", ""));
            Context.Departments.Add(new Department("Системное программирование", Context.Faculties[0], ""));
            Context.Departments.Add(new Department("Математическое обеспечение информационных технологий", Context.Faculties[0], ""));
            Context.Degrees.Add(new Degree("02.03.02", "Фундаментальная информатика и информационные технологии", Context.Departments[0], ""));
        }
        void GetStudy()
        {
            Context.Subjects.Add(new Subject("Алгебра и геометрия", Context.Degrees[0], "", 1));
            Context.Exams.Add(new Exam("Итоговый тест", ExamType.Exam, Context.Subjects[0], "", 40));
            Context.Assignments.Add(new Assignment("Матрицы", AssignmentType.CheckPoint, Context.Subjects[0], 15));
            Context.Assignments.Add(new Assignment("Векторы", AssignmentType.CheckPoint, Context.Subjects[0], 15));
            Context.Assignments.Add(new Assignment("Системы линейных алгебраических уравнений", AssignmentType.CheckPoint, Context.Subjects[0], 15));
            Context.Assignments.Add(new Assignment("Уравнение прямой на плоскости", AssignmentType.CheckPoint, Context.Subjects[0], 15));
        }
        void GetActualIdentities()
        {
            Context.Users.Add(new User(10001, "Андрей", "123", "shalyutov.a@ya.ru", "+79000000000"));
            Context.Users.Add(new User(10002, "Ирина", "456", "irina@ya.ru", "+79100000007"));
            Context.Users.Add(new User(10003, "Игорь", "789", "igor@ya.ru", "+79059000521"));
        }
        void GetActualStudy()
        {
            Context.Courses.Add(new Course(Context.Subjects[0], 2023));
            Context.Assessments.Add(new Assessment(Context.Courses[0], Context.Assignments[0], 15, Context.Users[0]));
            Context.Assessments.Add(new Assessment(Context.Courses[0], Context.Assignments[1], 12, Context.Users[0]));
            Context.Assessments.Add(new Assessment(Context.Courses[0], Context.Assignments[2], 11, Context.Users[0]));
            Context.Assessments.Add(new Assessment(Context.Courses[0], Context.Assignments[3], 14, Context.Users[0]));
            Context.ExamStatements.Add(new ExamStatement(Context.Courses[0], Context.Exams[0], 34, Context.Users[0]));
        }
        void GetGroups()
        {
            Context.Groups.Add(new AcademicGroup("КЭ-301", "", Context.Degrees[0], 3));
            Context.Groups.Add(new AcademicGroup("КЭ-302", "", Context.Degrees[0], 3));
        }
        void GetClaims()
        {
            Context.ClaimTypes.Add(new ClaimType("Участник курса", "Обладатель утверждения получает доступ к курсу"));
            Context.ClaimTypes.Add(new ClaimType("Сотрудник кафедры", "Обладатель утверждения прикреплён к кафедре"));
            Context.ClaimTypes.Add(new ClaimType("Студент группы", "Обладатель утверждения состоит в академической группе"));
            Context.CourseAccess.Add(new AccessClaim(Context.ClaimTypes[0], Context.Users[0], Context.Courses[0], AccessLevel.Student));
            Context.CourseAccess.Add(new AccessClaim(Context.ClaimTypes[0], Context.Users[1], Context.Courses[0], AccessLevel.Teacher));
            Claim c = new GroupClaim(Context.ClaimTypes[2], Context.Users[0], Context.Groups[1]);
            Context.Claims.Add(c);
            c = new GroupClaim(Context.ClaimTypes[2], Context.Users[2], Context.Groups[0]);
            Context.Claims.Add(c);
            c = new DepartmentClaim(Context.ClaimTypes[1], Context.Users[1], Context.Departments[1]);
            Context.Claims.Add(c);
        }
        public void Open()
        {
            string text = File.OpenText("data.json").ReadToEnd();
            var context = JsonConvert.DeserializeObject<Context>(text);
        }
        public void Save()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            File.WriteAllText("data.json", json);
        }
    }
}
