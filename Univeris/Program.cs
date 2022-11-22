using Univeris;
Data data = new Data();
Console.WriteLine("Найдём успеваемость Андрея по Алгебре и геометрии");
int sum = 0;
sum += data.Context.Assessments.Sum((grade) => { if (grade.User == data.Context.Users[0] && grade.Course == data.Context.Courses[0]) return grade.Value; else return 0; });
sum += data.Context.ExamStatements.Sum((grade) => { if (grade.User == data.Context.Users[0] && grade.Course == data.Context.Courses[0]) return grade.Value; else return 0; });
if (sum >= 85)
    Console.WriteLine("Отлично");
else if (sum >= 75)
    Console.WriteLine("Хорошо");
else if (sum >= 60)
    Console.WriteLine("Удовлетворительно");
else
    Console.WriteLine("Неудовлетворительно");