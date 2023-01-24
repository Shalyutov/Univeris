using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univeris.Global
{
    /// <summary>
    /// Шаг критерия
    /// </summary>
    class CriteriaStep : IEquatable<CriteriaStep?>, IComparable<CriteriaStep>
    {
        /// <summary>
        /// Название критерия
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = "";
        /// <summary>
        /// Эквивалентное количество баллов на шаге критерия
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// Конструктор шага критерия
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="description">Описание</param>
        /// <param name="score">Количество баллов</param>
        public CriteriaStep(string name, string description, int score)
        {
            Name = name;
            Description = description;
            Score = score;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as CriteriaStep);
        }

        public bool Equals(CriteriaStep? other)
        {
            return other is not null &&
                   Name == other.Name &&
                   Description == other.Description &&
                   Score == other.Score;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Score);
        }

        public static bool operator ==(CriteriaStep? left, CriteriaStep? right)
        {
            return EqualityComparer<CriteriaStep>.Default.Equals(left, right);
        }

        public static bool operator !=(CriteriaStep? left, CriteriaStep? right)
        {
            return !(left == right);
        }

        public int CompareTo(CriteriaStep? step)
        {
            if (step is null) throw new ArgumentException("Некорректное значение параметра");
            return Score - step.Score;
        }
    }
    /// <summary>
    /// Критерий оценивания контрольной точки
    /// </summary>
    class AssignmentCriteria
    {
        /// <summary>
        /// Название критерия
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// Краткое описание
        /// </summary>
        public string Description { get; set; } = "";
        private List<CriteriaStep> _steps;
        /// <summary>
        /// Шаги критерия
        /// </summary>
        public List<CriteriaStep> Steps 
        {
            get 
            { 
                return _steps;
            } 
            private set 
            {
                Steps = value;
                Steps.Sort((x, y) => x.CompareTo(y));//сортировка после изменения
            }
        }
        /// <summary>
        /// Конструктор критерия контрольной точки
        /// </summary>
        /// <param name="name">Название критерия</param>
        /// <param name="description">Описание</param>
        /// <param name="steps">Шаги критерия</param>
        /// <exception cref="ArgumentNullException"></exception>
        [JsonConstructor]
        public AssignmentCriteria(string name, string description, List<CriteriaStep> steps)
        {
            Name = name;
            Description = description;
            _steps = steps ?? throw new ArgumentNullException("Неверный список шагов критерия");
        }
        /// <summary>
        /// Конструктор критерия контрольной точки с пустым списком шагов
        /// </summary>
        /// <param name="name">Название критерия</param>
        /// <param name="description">Описание</param>
        public AssignmentCriteria(string name, string description)
        {
            Name = name;
            Description = description;
            _steps = new List<CriteriaStep>();
        }
        /// <summary>
        /// Добавить шаг критерия
        /// </summary>
        /// <param name="step">Шаг</param>
        public void AddStep(CriteriaStep step)
        {
            Steps.Add(step);
            Steps.Sort((x, y) => x.CompareTo(y));//сортировка после изменения
        }
        /// <summary>
        /// Удаление шага критерия по объекту
        /// </summary>
        /// <param name="step">Шаг</param>
        public void DeleteStep(CriteriaStep step)
        {
            Steps.Remove(step);
            Steps.Sort((x, y) => x.CompareTo(y));
        }
        /// <summary>
        /// Удаление шага критерия по индексу
        /// </summary>
        /// <param name="index">Индекс шага</param>
        public void DeleteStep(int index)
        {
            Steps.RemoveAt(index);
            Steps.Sort((x, y) => x.CompareTo(y));
        }
        //TODO: можно реализовать редактирование
    }
    /// <summary>
    /// Контрольная точка с критериями оценивания
    /// </summary>
    internal class ExtendedAssignment : Assignment
    {
        /// <summary>
        /// Список критериев
        /// </summary>
        public List<AssignmentCriteria> Criterias { get; set; }
        /// <summary>
        /// Выделенное количество баллов
        /// </summary>
        override public int Score
        { 
            get 
            {
                if (!Criterias.Any()) return Score;//если критериев нет, то возвращаем первоначальное количество баллов
                return Criterias.Sum(criteria => criteria.Steps.Last().Score); //сумма максимального оценивания по всем критериям
            }
            set
            {
                Score = value;
            }
        }
        /// <summary>
        /// Конструктор контрольной точки с критериями оценивания
        /// </summary>
        /// <param name="name">Название точки</param>
        /// <param name="type">Тип</param>
        /// <param name="subject">Дисциплина</param>
        /// <param name="score">Выделенное количество баллов</param>
        /// <param name="criterias">Список критериев</param>
        [JsonConstructor]
        public ExtendedAssignment(string name, AssignmentType type, Subject subject, int score, List<AssignmentCriteria> criterias) : base(name, type, subject, score)
        {
            Criterias = criterias;
        }
        /// <summary>
        /// Конструктор контрольной точки с критериями оценивания
        /// </summary>
        /// <param name="name">Название точки</param>
        /// <param name="type">Тип</param>
        /// <param name="subject">Дисциплина</param>
        /// <param name="criterias">Список критериев</param>
        public ExtendedAssignment(string name, AssignmentType type, Subject subject, List<AssignmentCriteria> criterias) : base(name, type, subject)
        {
            Criterias = criterias;
            Score = Score;//фиксация значения в структуре, присваиваем вычисленное getter значение (необязательно, можно убрать)
        }
        /// <summary>
        /// Конструктор контрольной точки с критериями оценивания (инициализация с пустым списком критериев)
        /// </summary>
        /// <param name="name">Название точки</param>
        /// <param name="type">Тип</param>
        /// <param name="subject">Дисциплина</param>
        public ExtendedAssignment(string name, AssignmentType type, Subject subject) : base(name, type, subject)
        {
            Criterias = new List<AssignmentCriteria>();
            Score = Score;//фиксация значения в структуре, присваиваем вычисленное getter значение (необязательно, можно убрать)
        }
    }
}
