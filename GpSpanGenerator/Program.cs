using System.Text.RegularExpressions;

namespace GpSpanGenerator
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var regOne =
                @"\-?\d+\,?\d \s?(тысяч кубических метров|тыс\. рабочих мест|тысячи человек|тысяч единиц
                |млрд\. рублей|млн человек|млн долл\.|тыс\. гектаров|тыс\. человек|млрд долларов|млн граждан
                |млн гражданам|млрд\. руб\.|млн рублей|млн\. тонн|тыс\. т\.|тыс\. штук|тыс\. шт\.|тыс\. тонн|млн услуг
                |млн тонн|кв\. километров|тыс\. чел\.|млрд рублей|млрд руб\.|млн чел\.|тысяч человек|тыс\. населения
                |тыс\. единиц|усл\. ед\.|процентов|процента|месяцев|миллиона|человек|пункта|гектаров|гектара|единицы
                |единица|единицу|единиц|тысячи|объекта|объектов|тыс\. ед\.|млн\. кв\. метров|тысяч|тонн\.|тонн|годов|годом
                |годах|году|года|годам|год|лет|МВт|тыс\.|млрд\.|млн\.|чел\.|ед\.|г\.|км\.|\%|км|)";

            var regTwo = @"\s?\-?\d+\,?\s?\d*\s?(тысяч кубических метров|тыс\. рабочих мест|тысячи человек|тысяч единиц|млрд\. рублей
                |млн человек|млн долл\.|тыс\. гектаров|тыс\. человек|млрд долларов|млн граждан|млрд\. руб\.|млн рублей|млн\. тонн
                |тыс\. т\.|тыс\. штук|тыс\. шт\.|тыс\. тонн|млн услуг|млн тонн|кв\. километров|тыс\. чел\.|млрд рублей|млрд руб\.
                |млн чел\.|тысяч человек|тыс\. населения|тыс\. единиц|усл\. ед\.|процентов|процента|месяцев|миллиона|человек|пункта
                |гектаров|гектара|единицы|единица|единицу|единиц|тысячи|объекта|объектов|тыс\. ед\.|млн\. кв\. метров|тысяч|тонн\.|тонн
                |годов |годом |годах |году |года |годам |год |лет |МВт |тыс\.|млрд\.|млн\.|чел\.|ед\.|г\.|км\.|\%|км|)\s?";

            var regThree = @"\-?\d+\,?\d \";

            var str = @"В рамках госпрограммы в 2022 г.: 
                        • 13 101 человек дополнительно эвакуировали с использованием санитарной авиации 
                            (при плановом значении – 8 450 человек);
                        • доля детских поликлиник и детских поликлинических отделений с созданной современной инфраструктурой 
                            оказания медицинской помощи детям достигла 98,7 % (при плановом значении – 95,0 %);
                        • 1048,4 тыс. ед. – объем оказанной высокотехнологичной медицинской помощи населению (при плановом 
                            значении – 890,0 тыс. ед.);
                        • 44,09 % охват граждан информацией о возможностях медицинской реабилитации в личном кабинете 
                            «Мое здоровье» на Едином портале государственных и муниципальных услуг (функций) 
                            (при плановом значении – 35 %);
                        • 40,7 число случаев на 100 тысяч детей соответствующего возраста смертность детей в возрасте 0-17 лет 
                            на 100 000 детей соответствующего возраста (при плановом значении – 49,8 число случаев 
                            на 100 тысяч детей соответствующего возраста);
                        • 5,7 промилле (0,1 процента) смертность детей в возрасте 0-4 года на 1000 родившихся живыми 
                            (при плановом значении – 6 промилле (0,1 процента)).";

            var str2 = "какой-то текст";


            var fileIn  = "in3.sql";
            var fileOut = "out3.sql";

            // await SpanRender(fileIn, fileOut, regTwo);
            Console.WriteLine(SpanGenerator(str, regTwo));
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

        private static async Task SpanRender(string fileIn, string fileOut, string reg)
        {
            var regex = new Regex(reg);

            using (var reader = new StreamReader(fileIn))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    Console.WriteLine("--------------------------");
                    // Console.WriteLine(line);

                    var matches = regex.Matches(line);
                    var result  = line;

                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                        {
                            if (match.Value != null)
                            {
                                // Console.WriteLine(match.Value);
                                // Console.WriteLine("<span>" + match.Value + "</span>");
                                var target = "<span>" + match.Value + "</span>";
                                result = result.Replace(match.Value, target);

                                // Console.WriteLine(match.Value);
                                // Console.WriteLine("<span>" + match.Value + "</span>");
                                // var target = "<span>" + match.Value + "</span>";
                                // var result = regex.Replace(line, target);
                            }
                        }


                        using (var writer = new StreamWriter(fileOut, true))
                        {
                            Console.WriteLine(result);
                            await writer.WriteLineAsync(result);
                        }
                    }
                    else
                    {
                        // Console.WriteLine("Совпадений не найдено");

                        using (var writer = new StreamWriter(fileOut, true))
                        {
                            Console.WriteLine(line);
                            await writer.WriteLineAsync(line);
                        }
                    }
                }
            }
        }
    }
}