using App.Metrics;
using App.Metrics.Counter;

namespace Prometheus;

public static class MetricsRegistry
{
    public static CounterOptions SampleCounter => new CounterOptions
    {
        Name = "Sample Counter",
        MeasurementUnit = Unit.Calls
    };
}
