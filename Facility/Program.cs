using System.Net;
using Facility.Here;
using Newtonsoft.Json;

namespace Facility
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            
            var facilityFiles = new[]
            {
                "facilityProduction.csv",
                "facilityRC13.csv",
                "facilityStaging.csv",
                "FacilityDev.csv"
            };
            var facilityAddress = new List<FacilityAddress>();

            foreach (var file in facilityFiles)
            {
                var facility = File.ReadAllLines($"./facility/{file}");

                switch (file)
                {
                    case "facilityProduction.csv":

                        foreach (var line in facility)
                        {
                            var data = line.Split("\t").ToList();

                            if (data[0] == "a3324204-3ab1-465a-86f9-5fdd54bd8573") continue;
                            
                            var geoCoder =  await GeoCoder(data[14]);
                            
                            // Console.WriteLine(data[0] + '\t' + data[14] + '\t' + geoCoder);

                            Console.WriteLine($"migrationBuilder.UpdateData(\"Facility\", \"Id\", \"{data[0]}\", \"RegionCode\", \"{geoCoder}\");");
                        }

                        break;

                    case "facilityRC13.csv":

                        foreach (var line in facility)
                        {
                            var data = line.Split("\t").ToList();
                            var geoCoder =  await GeoCoder(data[14]);
                            
                            // Console.WriteLine(data[0] + '\t' + data[14] + '\t' + geoCoder);
                            Console.WriteLine($"migrationBuilder.UpdateData(\"Facility\", \"Id\", \"{data[0]}\", \"RegionCode\", \"{geoCoder}\");");
                        }

                        break;

                    case "facilityStaging.csv":

                        foreach (var line in facility)
                        {
                            var data = line.Split("\t").ToList();
                            var geoCoder =  await GeoCoder( data[13] + ',' + data[21] + ',' + data[32] + ',' +
                                                            data[35]);
                            
                            // Console.WriteLine(data[0] + '\t' + data[13] + '\t' + data[21] + '\t' + data[32] + '\t' +
                            //                   data[35] + '\t' + geoCoder);
                            
                            Console.WriteLine($"migrationBuilder.UpdateData(\"Facility\", \"Id\", \"{data[0]}\", \"RegionCode\", \"{geoCoder}\");");
                        }

                        break;

                    case "FacilityDev.csv":

                        foreach (var line in facility)
                        {
                            var data = line.Split(",").ToList();

                            if (data[0] == "dc096ae3-7a2a-4805-a0f1-4ebf6168e8c8") continue; // broken record

                            var geoCoder =  await GeoCoder( data[13] + ',' + data[21] + ',' + data[32] + ',' +
                                                            data[35]);
                            
                            // Console.WriteLine(data[0] + '\t' + data[13] + '\t' + data[21] + '\t' + data[32] + '\t' +
                            //                   data[35] + '\t' + geoCoder);
                            
                            Console.WriteLine($"migrationBuilder.UpdateData(\"Facility\", \"Id\", \"{data[0]}\", \"RegionCode\", \"{geoCoder}\");");
                        }

                        break;
                }
            }

            var debug = true;
        }

        private class FacilityAddress
        {
            public Guid Id { get; set; }
            public string? Address { get; set; }
            public string? City { get; set; }
            public string? ZipCode { get; set; }
            public string? CountryCode { get; set; }
            public string? RegionCode { get; set; }
        }

        private static async Task<string?> GeoCoder(string address)
        {
            const string baseUrl = "https://geocode.search.hereapi.com/v1/geocode?";
            const string apiKey = "uCVmV6KQobaqn2TQ74-o1rtMpv1hS4Y-tTEvR-pYeAI";
            
            using var client = new System.Net.Http.HttpClient();

            var url = $"{baseUrl}q={address}&apiKey={apiKey}";
            
            var request = new HttpRequestMessage();
            
            request.RequestUri = new Uri(url);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
 
            var response =  await client.SendAsync(request);

            if (response.StatusCode != HttpStatusCode.OK) return null;
            
            var responseContent = response.Content;
            var json =  responseContent.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<Dictionary<string, List<HereGeoCoderDto>>>(json)["items"];

            foreach (var location in result)
            {
                return location.Address.StateCode;
            }

            return null;
        }
    }
}

// var searchLocation = string.Join(", ", address.Trim(), city.Trim(), postalCode.Trim(), country.Name);
// https://geocode.search.hereapi.com/v1/geocode?q=Canada L9T 8W7 8460 Mount Pleasant Way Unit 2&apiKey=uCVmV6KQobaqn2TQ74-o1rtMpv1hS4Y-tTEvR-pYeAI
/*

dc096ae3-7a2a-4805-a0f1-4ebf6168e8c8 -  не обрабатывать

*/