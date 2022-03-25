namespace Prometheus.Extensions
{
    public static class AspNetCoreMetricsBuilderExtensions
    {
        public static void UseAppMetrics(this IApplicationBuilder app)
        {
            app.UseMetricsAllEndpoints();
            app.UseMetricsActiveRequestMiddleware();
            app.UseMetricsErrorTrackingMiddleware();
            app.UseMetricsPostAndPutSizeTrackingMiddleware();
            app.UseMetricsRequestTrackingMiddleware();
        }
    }
}