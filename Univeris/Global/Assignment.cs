using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris.Global
{
    /// <summary>
    /// Тип контрольной точки
    /// </summary>
    public enum AssignmentType
    {
        /// <summary>
        /// Контрольная работа
        /// </summary>
        CheckPoint = 0,
        /// <summary>
        /// Тест
        /// </summary>
        Test = 1,
        /// <summary>
        /// Практическое задание
        /// </summary>
        Practice = 2,
        /// <summary>
        /// Лабораторная работа
        /// </summary>
        Lab = 3,
        /// <summary>
        /// Тест-викторина
        /// </summary>
        Quiz = 4,
        /// <summary>
        /// Устный ответ
        /// </summary>
        Answer = 5,
        /// <summary>
        /// Письменное задание
        /// </summary>
        Paper = 6
    }
    /// <summary>
    /// Контрольная точка
    /// </summary>
    public class Assignment : IEquatable<Assignment?>
    {
        /// <summary>
        /// Название контрольной точки
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// Тип контрольной точки
        /// </summary>
        public AssignmentType Type { get; set; }
        /// <summary>
        /// Связанная дисциплина
        /// </summary>
        public Subject Subject { get; set; }
        /// <summary>
        /// Выделенное количество баллов (зависит от Положения БРС)
        /// </summary>
        virtual public int Score { get; set; }
        /// <summary>
        /// Конструктор контрольной точки
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="type">Тип</param>
        /// <param name="subject">Дисциплина</param>
        /// <param name="score">Выделенные баллы</param>
        /// <exception cref="ArgumentNullException">Неверное задание имени или предмета</exception>
        [JsonConstructor]
        public Assignment(string name, AssignmentType type, Subject subject, int score)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            Score = score;
        }
        /// <summary>
        /// Конструктор контрольной точки с исходным количеством баллов = 0
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="type">Тип</param>
        /// <param name="subject">Дисциплина</param>
        /// <exception cref="ArgumentNullException">Неверное задание имени или предмета</exception>
        public Assignment(string name, AssignmentType type, Subject subject)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            Score = 0;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Assignment);
        }

        public bool Equals(Assignment? other)
        {
            return other is not null &&
                   Name == other.Name &&
                   Type == other.Type &&
                   EqualityComparer<Subject>.Default.Equals(Subject, other.Subject) &&
                   Score == other.Score;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Type, Subject, Score);
        }

        public static bool operator ==(Assignment? left, Assignment? right)
        {
            return EqualityComparer<Assignment>.Default.Equals(left, right);
        }

        public static bool operator !=(Assignment? left, Assignment? right)
        {
            return !(left == right);
        }
    }
}
