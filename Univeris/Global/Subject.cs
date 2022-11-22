using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris.Global
{
    /// <summary>
    /// Дисциплина образовательной программы
    /// </summary>
    internal class Subject
    {
        /// <summary>
        /// Название дисциплины
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// Образовательная программа
        /// </summary>
        public Degree Degree { get; set; }
        /// <summary>
        /// Краткое описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Семестр изучения
        /// </summary>
        public int Semester { get; set; }
        /// <summary>
        /// Объём лекционной работы
        /// </summary>
        public int Lectures { get; set; }
        /// <summary>
        /// Объём практических занятий
        /// </summary>
        public int Practice { get; set; }
        /// <summary>
        /// Объём лабораторных занятий
        /// </summary>
        public int Laboratory { get; set; }
        /// <summary>
        /// Объём самостоятельной работы студента
        /// </summary>
        public int SelfStudent { get; set; }
        /// <summary>
        /// Констурктор дисциплины
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="degree">Направление</param>
        /// <param name="description">Описание</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Subject(string name, Degree degree, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Degree = degree ?? throw new ArgumentNullException(nameof(degree));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Semester = 0;
        }
        public Subject(string name, Degree degree, string description, int semester) : this(name, degree, description)
        {
            Semester = semester;
        }
        public Subject(string name, Degree degree, string description, int semester, int lectures, int practice, int laboratory, int selfStudent) : this(name, degree, description, semester)
        {
            Lectures = lectures;
            Practice = practice;
            Laboratory = laboratory;
            SelfStudent = selfStudent;
        }
    }
}
