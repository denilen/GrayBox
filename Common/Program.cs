namespace Common
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var date = DateTime.Now;
            Console.WriteLine(date.GetQuarter());
            Console.WriteLine(date.GetFinancialQuarter());
        }

        public class PeriodDto
        {
            public string   Id    { get; set; }
            public string   Name  { get; set; }
            public DateTime Start { get; set; }
            public DateTime End   { get; set; }
        }

        private static int GetQuarter(this DateTime date)
        {
            return (date.Month + 2) / 3;
        }

        private static int GetFinancialQuarter(this DateTime date)
        {
            return (date.AddMonths(-3).Month + 2) / 3;
        }
    }
}