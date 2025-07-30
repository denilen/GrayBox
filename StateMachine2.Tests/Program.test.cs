namespace StateMachine2.Tests;

public class StateMachineTests
{
    [Fact]
    public void ShouldCycleThroughAllStatesWhenOption1IsSelectedMultipleTimes()
    {
        // Arrange
        var output = new StringWriter();
        Console.SetOut(output);

        var input = new StringReader("1\n1\n1\n3\n");
        Console.SetIn(input);

        // Act
        StateMachine.Main();

        // Assert
        var outputString = output.ToString();
        Assert.Contains("Текущее состояние: State1", outputString);
        Assert.Contains("Текущее состояние: State2", outputString);
        Assert.Contains("Текущее состояние: State3", outputString);
        Assert.Contains("Текущее состояние: State1", outputString);
    }
}
