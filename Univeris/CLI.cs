using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univeris.Actual;
using Univeris.Identity;
using Univeris.Identity.Claims;

namespace Univeris
{
    internal class CLI
    {
        private User? user;
        private Data data;

        public CLI()
        {
            data = new Data();
        }
        public void Handle()
        {
            Console.WriteLine("КИАС Универис");
            Console.WriteLine("Добро пожаловать");
            Home();
        }
        public void Home()
        {
            Console.WriteLine("Вводите команды для взаимодействия с системой. Для выхода введите \"Закрыть\"");
            var command = Console.ReadLine();
            while (command != "Закрыть")
            {
                switch (command)
                {
                    case "Войти":
                        Navigate(SignIn);
                        break;
                    case "Курсы":
                        Navigate(Cources);
                        break;
                    case "Успеваемость":
                        Navigate(Performance);
                        break;
                    case "Выйти":
                        Navigate(SignOut);
                        break;
                }
                if (command == "Закрыть") break;
                Console.WriteLine("Вводите команды для взаимодействия с системой. Для выхода введите \"Закрыть\"");
                command = Console.ReadLine();
            }
        }
        public void Navigate(Action action)
        {
            Console.Clear();
            if (user == null)
            {
                Console.WriteLine("КИАС Универис");
                Console.WriteLine("Вы не авторизованы");
                if (action == SignOut) return;
                SignIn();
                if (user == null) return;
            }
            Console.Clear();
            Console.WriteLine($"КИАС Универис\tВход выполнен\tАккаунт: {user.Username}");
            action();
        }
        public void SignIn()
        {
            Console.WriteLine("Вход в систему");
            Console.Write("Имя пользователя:");
            string username = Console.ReadLine() ?? "";
            Console.Write("Пароль: ");
            string password = ReadPassword();
            if (SignIn(username, password))
            {
                Console.WriteLine("");
                return;
            }
            else
            {
                Console.WriteLine("Неправильное имя пользователя или пароль");
                Navigate(SignIn);
            }
        }
        public string ReadPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return pass;
        }
        public bool SignIn(string username, string password)
        {
            if (string.IsNullOrEmpty(username)) return false;
            if (string.IsNullOrEmpty(password)) return false;
            user = data.Context.Users.Find(user => user.Username == username && user.IsPasswordValid(password));
            return true;
        }
        public void SignOut()
        {
            user = null;
            Console.WriteLine("Вы вышли из системы");
            return;
        }
        public void Cources()
        {
            var courses = GetAllCourses();
            Console.WriteLine("Мои курсы:");
            int i = 0;
            foreach (var course in courses)
            {
                i++;
                Console.WriteLine($"[{i}]\t{course.Subject.Name}\t{course.Year}");
            }
            if (i == 0) Console.WriteLine("Вы не участвуете ни в одном курсе");
            return;
        }
        public List<Course> GetAllCourses()
        {
            List<Course> courses = new();
            var accessClaims = data.Context.CourseAccess.FindAll(claim => claim.User == user);
            foreach (var claim in accessClaims) courses.Add((Course)claim.Value);
            return courses;
        }
        public List<Course> GetStudyCourses()
        {
            List<Course> courses = new();
            var accessClaims = data.Context.CourseAccess.FindAll(claim => claim.User == user && claim.Level == AccessLevel.Student);
            foreach (var claim in accessClaims) courses.Add((Course)claim.Value);
            return courses;
        }
        public void Performance()
        {
            var courses = GetStudyCourses();
            Console.WriteLine("Успеваемость");
            int i = 0;
            foreach (var course in courses)
            {
                i++;
                int sum = 0;
                Console.WriteLine($"[{i}]\t{course.Subject.Name}\t{course.Year}\n");
                var assessments = data.Context.Assessments.FindAll(assessment=> assessment.User == user && assessment.Course == course);
                Console.WriteLine("Текущие оценки");
                foreach (var assessment in assessments)
                {
                    sum += assessment.Value;
                    Console.WriteLine($"\t{assessment.Assignment.Name}\t{assessment.Value}");
                    
                }
                var examresult = data.Context.ExamStatements.Find(exam=>exam.User == user && exam.Course == course);
                if (examresult != null)
                {
                    Console.WriteLine("\nЭкзамен");
                    Console.WriteLine($"\t{examresult.Exam.Name}\t{examresult.Value}");
                    
                    sum+= examresult.Value;
                }
                else
                {
                    Console.WriteLine("\nПромежуточная аттестация ещё не наступила");
                }
                Console.Write($"\nИтого {sum} баллов\n\nОценка: ");
                if (sum >= 85)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Отлично\n");
                    Console.ResetColor();
                }
                else if (sum >= 75)
                    Console.WriteLine("Хорошо\n");
                else if (sum >= 60)
                    Console.WriteLine("Удовлетворительно\n");
                else
                    Console.WriteLine("Неудовлетворительно\n");
            }
            if (i == 0) Console.WriteLine("Вы не участвуете ни в одном курсе");
        }
    }
}
