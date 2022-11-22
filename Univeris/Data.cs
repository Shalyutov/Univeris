using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univeris.Global;
using Univeris.Actual;
using Univeris.Identity;

namespace Univeris
{
    internal class Data
    {
        public Context Context { get; set; }
        public Data()
        {
            Context = new Context();
            GetFaculty();
            GetStudy();
            GetActualIdentities();
            GetActualStudy();
        }
        void GetFaculty()
        {
            Context.Faculties.Add(new Faculty("Высшая школа электроники и компьютерных наук", ""));
            Context.Departments.Add(new Department("Системное программирование", Context.Faculties[0], ""));
            Context.Degrees.Add(new Degree("01.02.03", "Фундаментальная информатика и информационные технологии", Context.Departments[0], ""));
        }
        void GetStudy()
        {
            Context.Subjects.Add(new Subject("Алгебра и геометрия", Context.Degrees[0], "", 1));
            Context.Exams.Add(new Exam("Итоговый тест в аудитории", ExamType.Exam, Context.Subjects[0], "", 40));
            Context.Assignments.Add(new Assignment("Матрицы и векторы", AssignmentType.CheckPoint, Context.Subjects[0]));
        }
        void GetActualIdentities()
        {
            Context.Users.Add(new User(10001, "Андрей", "Icvm1864", "shalyutov.a@ya.ru", "+79000000000"));
        }
        void GetActualStudy()
        {
            Context.Courses.Add(new Course(Context.Subjects[0], 2023));
            Context.Assessments.Add(new Assessment(Context.Courses[0], Context.Assignments[0], 42, Context.Users[0]));
            Context.ExamStatements.Add(new ExamStatement(Context.Courses[0], Context.Exams[0], 33, Context.Users[0]));
        }
    }
}
