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
        /// Выделенное количество баллов (вес, БРС, обычно = 40)
        /// </summary>
        public int Score { get; set; }
    }
}
