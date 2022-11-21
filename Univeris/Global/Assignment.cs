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
    enum AssignmentType
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
    internal class Assignment
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
        public int Score { get; set; }
    }
}
