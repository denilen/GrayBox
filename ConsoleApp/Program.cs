using System.Text.RegularExpressions;


namespace ConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var reg =
                @"\-?\d+\,?\d*\s?(тысяч кубических метров|тыс\. рабочих мест|тысячи человек|тысяч единиц
                |млрд\. рублей|млн человек|млн долл\.|тыс\. гектаров|тыс\. человек|млрд долларов|млн граждан
                |млн гражданам|млрд\. руб\.|млн рублей|млн\. тонн|тыс\. т\.|тыс\. штук|тыс\. шт\.|тыс\. тонн|млн услуг
                |млн тонн|кв\. километров|тыс\. чел\.|млрд рублей|млрд руб\.|млн чел\.|тысяч человек|тыс\. населения
                |тыс\. единиц|усл\. ед\.|процентов|процента|месяцев|миллиона|человек|пункта|гектаров|гектара|единицы
                |единица|единицу|единиц|тысячи|объекта|объектов|тыс\. ед\.|млн\. кв\. метров|тысяч|тонн\.|тонн|годов|годом
                |годах|году|года|годам|год|лет|МВт|тыс\.|млрд\.|млн\.|чел\.|ед\.|г\.|км\.|\%|км|)";

            var reg2 = @"\-?\d+\,?\d \";

            var str = @"<p> <b>По результатам реализации госпрограммы планируется: </b> 
                            <br>• увеличить долю твердых коммунальных отходов, направленных <br>на обработку (сортировку), 
                            в общей массе образованных твердых коммунальных отходов до 50,2 процентов;<br>• сократить объемы 
                            сбросов загрязненных сточных вод в водные объекты Байкальской природной территории 
                            на 101 916,0 тысяч кубических метров по отношению к 2020 году;<br>• 
                            снизить совокупный объем выбросов за отчетный год до 80 процентов (на 16 процентов по отношению 
                            к 2021 году);<br>• увеличить количество особо охраняемых природных территорий федерального значения 
                            до 241 единиц;<br>• ликвидировать не менее 191 несанкционированной свалки в границах городов;<br>
                            • ликвидировать не менее 88 наиболее опасных объектов накопленного вреда окружающей среде;<br>
                            • охватить не менее 250 городов комплексной информационной системой мониторинга 
                            состояния окружающей среды.<br></p>";

            var str2 = "какой-то текст";

            var fileIn  = "in3.sql";
            var fileOut = "out3.sql";

            SpanRender(fileIn, fileOut, reg);
            // Console.WriteLine(SpanGenerator(str2, reg));
        }

        private static string SpanGenerator(string input, string reg)
        {
            var regex   = new Regex(reg);
            var matches = regex.Matches(input);
            var result  = input;

            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    Console.WriteLine(match.Value);
                    Console.WriteLine("<span>" + match.Value + "</span>");
                    var target = "<span>"      + match.Value + "</span>";
                    result = result.Replace(match.Value, target);
                }

                Console.WriteLine(result);
                return result;
            }

            Console.WriteLine("Совпадений не найдено");

            return input;
        }

        private static async void SpanRender(string fileIn, string fileOut, string reg)
        {
            var regex = new Regex(reg);

            using (var reader = new StreamReader(fileIn))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    Console.WriteLine("--------------------------");
                    Console.WriteLine(line);

                    var matches = regex.Matches(line);

                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                        {
                            // Console.WriteLine(match.Value);
                            // Console.WriteLine("<span>" + match.Value + "</span>");
                            var target = "<span>" + match.Value + "</span>";
                            var result = regex.Replace(line, target);
                            Console.WriteLine(result);

                            using (var writer = new StreamWriter(fileOut, true))
                            {
                                await writer.WriteLineAsync(result);
                            }
                        }
                    }
                    else
                    {
                        // Console.WriteLine("Совпадений не найдено");

                        using (var writer = new StreamWriter(fileOut, true))
                        {
                            await writer.WriteLineAsync(line);
                        }
                    }
                }
            }
        }
    }
}