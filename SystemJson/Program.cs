using System.Text.Json;
using SystemJson;

const string data =
    "{\"items\":[{\"title\":\"Москва, Центральный федеральный округ, Россия\",\"id\":\"here:cm:namedplace:20654514\"," +
    "\"resultType\":\"locality\",\"localityType\":\"city\",\"address\":{\"label\":"                                    +
    "\"Москва, Центральный федеральный округ, Россия\",\"countryCode\":\"RUS\",\"countryName\":\"Россия\","            +
    "\"state\":\"Центральный федеральный округ\",\"county\":\"Москва\",\"city\":\"Москва\",\"postalCode\":"            +
    "\"125009\"},\"position\":{\"lat\":55.75696,\"lng\":37.61502},\"mapView\":{\"west\":36.80166,\"south\":"           +
    "55.14286,\"east\":37.96761,\"north\":56.00985},\"scoring\":{\"queryScore\":1.0,\"fieldScore\":{\"city\":"         +
    "1.0}}}]}";


using var doc   = JsonDocument.Parse(data);
var       items = doc.RootElement.GetProperty("items").EnumerateArray();

var geoData        = new GeoCoderModel();
var geoCoderModels = new List<GeoCoderModel>();

foreach (var item in items)
{
    geoData.Address = item.GetProperty("title").ToString();
    geoData.Lat     = item.GetProperty("position").GetProperty("lat").GetDouble();
    geoData.Lon     = item.GetProperty("position").GetProperty("lng").GetDouble();

    geoCoderModels.Add(geoData);
    geoCoderModels.Add(geoData);
}

var result = geoCoderModels.Distinct();

var date = DateTime.Now;
var day  = date.Day;

Console.WriteLine(day);
                    
Console.WriteLine("stop");