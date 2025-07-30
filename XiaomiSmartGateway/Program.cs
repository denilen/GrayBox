using MiHomeLib;

namespace XiaomiSmartGateway;

internal static class Program
{
    internal static void Main(string[] args)
    {
        using var miHome = new MiHome();

        miHome.OnThSensor += (_, thSensor) =>
        {
            if (thSensor.Sid != "lumi.158d0007d3e9db") return; // sid of specific device

            Console.WriteLine(thSensor); // Sample output --> Temperature: 22,19°C, Humidity: 74,66%, Voltage: 3,035V

            thSensor.OnTemperatureChange += (_, e) =>
            {
                Console.WriteLine($"New temperature: {e.Temperature}");
            };

            thSensor.OnHumidityChange += (_, e) =>
            {
                Console.WriteLine($"New humidity: {e.Humidity}");
            };
        };
    }
}
