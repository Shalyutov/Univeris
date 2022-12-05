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
            Command();
        }
        public void Command()
        {
            var command = string.Empty;
            do
            {
                Console.WriteLine("Введите команду для работы с системой. Для выхода введите \"закрыть\"");
                command = Console.ReadLine();
                switch (command)
                {
                    case "войти":
                        Navigate(SignIn);
                        break;
                    case "курс":
                        Navigate(Cources);
                        break;
                    case "оценки":
                        Navigate(Performance);
                        break;
                    case "я":
                        Navigate(Account);
                        break;
                    case "выйти":
                        Navigate(SignOut);
                        break;
                }
                if (command == "закрыть") break;
            }
            while (command != "закрыть");
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
            if (action == SignIn) return;
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
            Console.Clear();
            Console.WriteLine("КИАС Универис");
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
                    Console.Write($"\t{assessment.Assignment.Name}");
                    if (assessment.Value*1.0 >= assessment.Assignment.Score*0.6)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine($"\t{assessment.Value}");
                    Console.ResetColor();
                }
                var examresult = data.Context.ExamStatements.Find(exam=>exam.User == user && exam.Course == course);
                if (examresult != null)
                {
                    sum += examresult.Value;
                    Console.WriteLine("\nЭкзамен");
                    Console.WriteLine($"\t{examresult.Exam.Name}\t{examresult.Value}");
                }
                else
                {
                    Console.WriteLine("\nСессия ещё не наступила");
                }
                Console.Write($"\nИтого {sum} баллов\n\nОценка за дисциплину: ");
                if (sum >= 85)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Отлично\n");
                    Console.ResetColor();
                }
                else if (sum >= 75)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Хорошо\n");
                    Console.ResetColor();
                }
                else if (sum >= 60)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Удовлетворительно\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Неудовлетворительно\n");
                    Console.ResetColor();
                } 
            }
            if (i == 0) Console.WriteLine("Вы не участвуете ни в одном курсе");
        }
        public void Account()
        {
            Console.WriteLine("Ваш аккаунт");
            Console.WriteLine($"Имя:\t{user?.Username}");
            Console.WriteLine($"Почта:\t{user?.Email}");
            Console.WriteLine($"Телефон:\t{user?.Phone}");
            
            var claims = data.Context.Claims.FindAll(claim => claim.User == user);
            if (claims.Count == 0)
            {
                Console.WriteLine("У вас нет утверждений в системе");
            }
            else
            {
                Console.WriteLine("\nВаши утверждения в системе");
                foreach (var claim in claims)
                {
                    Console.WriteLine("\t" + claim.ToString());
                }
            }
        }
    }
}
