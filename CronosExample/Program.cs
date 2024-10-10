using Cronos;

var expression = CronExpression.Parse("5 * * * *");

var nextUtc = expression.GetNextOccurrence(DateTime.UtcNow);

Console.WriteLine(nextUtc);