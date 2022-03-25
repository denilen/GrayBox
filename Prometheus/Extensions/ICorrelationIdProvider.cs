namespace Prometheus.Extensions
{
    public interface ICorrelationProvider
    {
        Guid? CorrelationId { get; }
        IDisposable CreateCorrelationScope(Guid? correlationId);
    }
}