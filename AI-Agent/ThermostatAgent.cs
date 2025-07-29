namespace IntelligentAgentDemo;

public class ThermostatAgent
{
    private readonly TemperatureSensor _sensor;
    private readonly Heater _heater;
    private readonly double _targetTemperature = 22.0;
    private readonly double _tolerance = 0.5;

    public ThermostatAgent(TemperatureSensor sensor, Heater heater)
    {
        _sensor = sensor;
        _heater = heater;
    }

    public void PerceiveAndAct()
    {
        double currentTemp = _sensor.ReadTemperature();
        Console.WriteLine($"🌡️ Температура: {currentTemp}°C");

        if (currentTemp < _targetTemperature - _tolerance)
            _heater.TurnOn();
        else if (currentTemp > _targetTemperature + _tolerance)
            _heater.TurnOff();
    }
}
