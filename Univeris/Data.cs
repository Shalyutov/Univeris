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
    public class Data
    {
        public Context Context { get; set; }
        public Data()
        {
            Context = new Context();
        }
        public void GetTemplate()
        {
            GetFaculty();
            GetStudy();
            GetActualIdentities();
            GetActualStudy();
            GetGroups();
            GetClaims();
        }
        void GetFaculty()
        {
            if (Context == null) return;
            Context.Faculties.Add(new Faculty("Высшая школа электроники и компьютерных наук", ""));
            Context.Departments.Add(new Department("Системное программирование", Context.Faculties[0], ""));
            Context.Departments.Add(new Department("Математическое обеспечение информационных технологий", Context.Faculties[0], ""));
            Context.Degrees.Add(new Degree("02.03.02", "Фундаментальная информатика и информационные технологии", Context.Departments[0], ""));
        }
        void GetStudy()
        {
            if (Context == null) return;
            Context.Subjects.Add(new Subject("Алгебра и геометрия", Context.Degrees[0], "", 1));
            Context.Exams.Add(new Exam("Итоговый тест", ExamType.Exam, Context.Subjects[0], "", 40));
            Context.Assignments.Add(new Assignment("Матрицы", AssignmentType.CheckPoint, Context.Subjects[0], 15));
            Context.Assignments.Add(new Assignment("Векторы", AssignmentType.CheckPoint, Context.Subjects[0], 15));
            Context.Assignments.Add(new Assignment("Системы линейных алгебраических уравнений", AssignmentType.CheckPoint, Context.Subjects[0], 15));
            Context.Assignments.Add(new Assignment("Уравнение прямой на плоскости", AssignmentType.CheckPoint, Context.Subjects[0], 15));

            Context.Subjects.Add(new Subject("Основы программирования", Context.Degrees[0], "", 2));
            Context.Exams.Add(new Exam("Итоговый тест", ExamType.Exam, Context.Subjects[1], "", 100));
            Context.Assignments.Add(new Assignment("Hello World", AssignmentType.Practice, Context.Subjects[1], 5));
            Context.Assignments.Add(new Assignment("Переменные и типы данных", AssignmentType.Practice, Context.Subjects[1], 10));
            Context.Assignments.Add(new Assignment("Управление потоком исполнения", AssignmentType.Practice, Context.Subjects[1], 15));
            Context.Assignments.Add(new Assignment("Структуры данных", AssignmentType.Practice, Context.Subjects[1], 20));
            Context.Assignments.Add(new Assignment("Алгоритмы", AssignmentType.Practice, Context.Subjects[1], 25));
        }
        void GetActualIdentities()
        {
            if (Context == null) return;
            Context.Users.Add(new User(10001, "андрей", "shalyutov.a@ya.ru", "+79000000000", "123"));
            Context.Users.Add(new User(10002, "ирина", "irina@ya.ru", "+79100000007", "456"));
            Context.Users.Add(new User(10003, "игорь", "igor@ya.ru", "+79059000521", "789"));
        }
        void GetActualStudy()
        {
            if (Context == null) return;
            Context.Courses.Add(new Course(Context.Subjects[0], 2023));
            Context.Assessments.Add(new Assessment(Context.Courses[0], Context.Assignments[0], 15, Context.Users[0]));
            Context.Assessments.Add(new Assessment(Context.Courses[0], Context.Assignments[1], 15, Context.Users[0]));
            Context.Assessments.Add(new Assessment(Context.Courses[0], Context.Assignments[2], 15, Context.Users[0]));
            Context.Assessments.Add(new Assessment(Context.Courses[0], Context.Assignments[3], 15, Context.Users[0]));
            Context.ExamStatements.Add(new ExamStatement(Context.Courses[0], Context.Exams[0], 39, Context.Users[0]));

            Context.Courses.Add(new Course(Context.Subjects[1], 2023));
            Context.Assessments.Add(new Assessment(Context.Courses[1], Context.Assignments[4], 5, Context.Users[0]));
            Context.Assessments.Add(new Assessment(Context.Courses[1], Context.Assignments[5], 10, Context.Users[0]));
            Context.Assessments.Add(new Assessment(Context.Courses[1], Context.Assignments[6], 13, Context.Users[0]));
        }
        void GetGroups()
        {
            if (Context == null) return;
            Context.Groups.Add(new AcademicGroup("КЭ-301", "", Context.Degrees[0], 3));
            Context.Groups.Add(new AcademicGroup("КЭ-302", "", Context.Degrees[0], 3));
        }
        void GetClaims()
        {
            if (Context == null) return;
            Context.ClaimTypes.Add(new ClaimType("Участник курса", "Обладатель утверждения получает доступ к курсу"));
            Context.ClaimTypes.Add(new ClaimType("Сотрудник кафедры", "Обладатель утверждения прикреплён к кафедре"));
            Context.ClaimTypes.Add(new ClaimType("Студент группы", "Обладатель утверждения состоит в академической группе"));
            Context.CourseAccess.Add(new AccessClaim(Context.ClaimTypes[0], Context.Users[0], Context.Courses[0], AccessLevel.Student));
            Context.CourseAccess.Add(new AccessClaim(Context.ClaimTypes[0], Context.Users[0], Context.Courses[1], AccessLevel.Student));
            Context.CourseAccess.Add(new AccessClaim(Context.ClaimTypes[0], Context.Users[1], Context.Courses[0], AccessLevel.Teacher));
            Context.CourseAccess.Add(new AccessClaim(Context.ClaimTypes[0], Context.Users[2], Context.Courses[1], AccessLevel.Teacher));
            Claim c = new GroupClaim(Context.ClaimTypes[2], Context.Users[0], Context.Groups[1]);
            Context.Claims.Add(c);
            c = new DepartmentClaim(Context.ClaimTypes[1], Context.Users[2], Context.Departments[1]);
            Context.Claims.Add(c);
            c = new DepartmentClaim(Context.ClaimTypes[1], Context.Users[1], Context.Departments[1]);
            Context.Claims.Add(c);
        }
        public void Open()
        {
            string text = File.OpenText("data.json").ReadToEnd();
            Context = JsonConvert.DeserializeObject<Context>(text) ?? new Context();
        }
        public void Open(string file)
        {
            string text = File.OpenText(file).ReadToEnd();
            Context = JsonConvert.DeserializeObject<Context>(text) ?? new Context();
        }
        public void Save()
        {
            string json = JsonConvert.SerializeObject(Context, Formatting.Indented, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
            File.WriteAllText("data.json", json);
        }
    }
}
