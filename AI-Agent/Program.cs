namespace AI_Agent;

internal static class Program
{
    private static void Main()
    {
        var env = new Environment();
        var sensor = new TemperatureSensor(env);
        var heater = new Heater();
        var agent = new ThermostatAgent(sensor, heater);

        // Emulation of time
        for (var minute = 0; minute < 10; minute++)
        {
            Console.WriteLine($"\n⏱️ Минута {minute}");

            agent.PerceiveAndAct();

            // We update the environment
            if (heater.IsOn)
                env.Temperature += 0.6;
            else
                env.Temperature -= 0.3;

            Thread.Sleep(500);
        }
    }
}
