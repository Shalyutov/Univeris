using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris.Global
{
    /// <summary>
    /// Тип аттестации
    /// </summary>
    enum ExamType
    {
        /// <summary>
        /// Экзамен
        /// </summary>
        Exam = 0,
        /// <summary>
        /// Зачёт
        /// </summary>
        Pass = 1,
        /// <summary>
        /// Дифференцированный зачёт
        /// </summary>
        DiffCredit = 2,
        /// <summary>
        /// Курсовая работа
        /// </summary>
        CourseWork = 3,
        /// <summary>
        /// Научно-исследовательская работа
        /// </summary>
        ScientificWork = 4,
        /// <summary>
        /// Выпускная работа
        /// </summary>
        GraduateWork = 5
    }
    /// <summary>
    /// Аттестация по дисциплине (экзамен/зачёт/дифференцированный зачёт/курсовая работа...)
    /// </summary>
    internal class Exam
    {
        /// <summary>
        /// Название аттестации
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// Тип аттестации
        /// </summary>
        public ExamType Type { get; set; }
        /// <summary>
        /// Связанная дисциплина 
        /// </summary>
        public Subject Subject { get; set; }
        /// <summary>
        /// Краткое описание
        /// </summary>
        public string Description { get; set; } = "";
        /// <summary>
        /// Выделенное количество баллов (вес см. БРС, обычно = 40)
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// Конструктор аттестации для дисциплины
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="type">Тип</param>
        /// <param name="subject">Дисциплина</param>
        /// <param name="description">Описание</param>
        /// <param name="score">Выделенные баллы</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Exam(string name, ExamType type, Subject subject, string description, int score)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Score = score;
        }
    }
}
