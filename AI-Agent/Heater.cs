namespace IntelligentAgentDemo;

public class Heater
{
    public bool IsOn { get; private set; }

    public void TurnOn()
    {
        IsOn = true;
        Console.WriteLine("🔆 Обогреватель включен.");
    }

    public void TurnOff()
    {
        IsOn = false;
        Console.WriteLine("🌑 Обогреватель выключен.");
    }
}
