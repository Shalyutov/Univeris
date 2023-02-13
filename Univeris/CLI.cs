using NLog;
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
        private readonly Logger logger;
        private Core core;
        public CLI()
        {
            Console.Title = "Универис";
            core = new Core();

            logger = LogManager.Setup().LoadConfiguration(builder => {
                builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: $"logs/log-{DateTime.Now}.txt");
            }).GetCurrentClassLogger();

            core.CoreInitialized += OnCoreInitialized;
            core.DataSelected += OnDataOperation;
            core.DataUpdated += OnDataOperation;
            core.ValueComputed += OnDataOperation;
            core.UserSignedIn += OnSignedIn;
            core.CoreAction += OnCoreAction;
            core.OnErrorOccured += OnCoreError;

            core.Start("data.json");//Now core starts with data from file
            //Core start with template data when param true
        }

        #region Command Line Handler
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
                    case "брс":
                        Navigate(Performance);
                        break;
                    case "я":
                        Navigate(Account);
                        break;
                    case "выйти":
                        Navigate(SignOut);
                        break;
                    case "?":
                        Navigate(Help);
                        break;
                }
                if (command == "закрыть") break;
            }
            while (command != "закрыть");
        }
        public void Navigate(Action action)
        {
            Console.Clear();
            if (action == Help)
            {
                Console.WriteLine("КИАС Универис");
                action();
                return;
            }
            if (core.User == null)
            {
                Console.WriteLine("КИАС Универис");
                Console.WriteLine("Вы не авторизованы");
                if (action == SignOut) return;
                SignIn();
                if (core.User == null) return;
            }
            Console.Clear();
            Console.WriteLine($"КИАС Универис\tВход выполнен\tАккаунт: {core.User.Username}");
            if (action == SignIn) return;
            action();
        }
        public void Help()
        {
            Console.WriteLine("Доступные команды\n");
            Console.WriteLine("\"войти\"\tВыполнить вход в систему используя имя пользователя и пароль");
            Console.WriteLine("\"выйти\"\tВыйти из информационной системы");
            Console.WriteLine("\"я\"\tПросмотреть информацию о профиле");
            Console.WriteLine("\"курс\"\tПросмотреть информацию о подключенных курсах");
            Console.WriteLine("\"брс\"\tПросмотреть информацию об успеваемости на изучаемых курсах");
            Console.WriteLine("\"?\"\tВывести информацию о доступных командах");
            Console.WriteLine();
            return;
        }
        #endregion

        #region IAM
        public void SignIn()
        {
            Console.WriteLine("Вход в систему");
            Console.Write("Имя пользователя:");
            string username = Console.ReadLine() ?? "";
            Console.Write("Пароль: ");
            string password = ReadPassword();
            if (core.SignIn(username, password))
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
        public void SignOut()
        {
            core.SignOut();
            Console.Clear();
            Console.WriteLine("КИАС Универис");
            Console.WriteLine("Вы вышли из системы");
            return;
        }
        public void Account()
        {
            Console.WriteLine("Ваш аккаунт");
            Console.WriteLine($"Имя:\t{core.User?.Username}");
            Console.WriteLine($"Почта:\t{core.User?.Email}");
            Console.WriteLine($"Телефон:\t{core.User?.Phone}");

            var claims = core.Data.Context.Claims.FindAll(claim => claim.User == core.User);
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
        #endregion

        #region PRS
        public void Cources()
        {
            var courses = core.GetCourses();
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
        public void Performance()
        {
            var courses = core.GetCourses(AccessLevel.Student);
            Console.WriteLine("Успеваемость");
            int i = 0;
            foreach (var course in courses)
            {
                i++;
                Console.WriteLine($"[{i}]\t{course.Subject.Name}\t{course.Year}\n");
                var assessments = core.GetAssessments(course);
                var assignments = core.GetAssignments(course.Subject);
                Console.WriteLine("Текущие оценки");
                for (int a = 0; a < assignments.Count; a++)
                {
                    Console.Write($"\t{assignments[a].Name}");
                    var assessment = assessments.Find(mark => mark.Assignment == assignments[a]);
                    if (assessment == null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("\tНе выполнено");
                        Console.ResetColor();
                        continue;
                    }
                    if (assessment.Value*1.0 >= assessment.Assignment.Score*0.6)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine($"\t{assessment.Value} из {assessment.Assignment.Score}");
                    Console.ResetColor();
                }
                float sum = 0.0f;
                var exam = core.GetExamStatement(course);
                if (exam != null)
                {
                    sum = core.GetRating(assignments, assessments, exam, false);
                    Console.WriteLine("\nЭкзамен");
                    Console.WriteLine($"\t{exam.Exam.Name}\t{exam.Value}");
                    Console.Write($"\nИтого: {sum} баллов\n\nОценка за дисциплину: ");
                }
                else
                {
                    sum = core.GetRating(assignments, assessments);
                    Console.WriteLine("\nАттестационное мероприятие не начато");
                    Console.Write($"\nТекущий контроль: {sum:F2} баллов\n\nПредварительная оценка за дисциплину: ");
                }
                
                
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
            return;
        }
        #endregion

        #region Logger Handlers
        void OnCoreInitialized()
        {
            logger.Debug("Ядро инициализировано");
        }
        void OnDataOperation(string entity)
        {
            logger.Debug(entity + " - Context CLI");
        }
        void OnCoreAction(string entity)
        {
            logger.Debug(entity + " - Ядро Универис");
        }
        void OnCoreError(string entity)
        {
            logger.Error(entity + " - Ядро Универис");
        }
        void OnSignedIn(string entity)
        {
            logger.Debug(entity + " - CLI");
        }
        #endregion
    }
}
