namespace AI_Agent;

/// <summary>
/// Sensor
/// </summary>
public class TemperatureSensor
{
    private Environment _env;

    public TemperatureSensor(Environment env)
    {
        _env = env;
    }

    public double ReadTemperature()
    {
        return _env.Temperature;
    }
}
