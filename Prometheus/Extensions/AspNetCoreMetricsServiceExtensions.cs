using App.Metrics;
using App.Metrics.Extensions.Configuration;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Mvc;

namespace Prometheus.Extensions
{
    public static class AspNetCoreMetricsServiceExtensions
    {
        public static void AddAppMetrics(this IServiceCollection services, IConfiguration configuration)
        {
            var metrics = AppMetrics.CreateDefaultBuilder()
                                    .OutputMetrics.AsPrometheusPlainText()
                                    .OutputMetrics.AsPrometheusProtobuf()
                                    .Configuration.ReadFrom(configuration)
                                    .Build();

            services.AddMetrics(metrics);
            services.AddMetricsTrackingMiddleware(configuration);
            services.AddMetricsEndpoints(configuration, options =>
            {
                options.MetricsTextEndpointOutputFormatter = metrics.OutputMetricsFormatters
                                                                    .OfType<MetricsPrometheusTextOutputFormatter>()
                                                                    .First();
                options.MetricsEndpointOutputFormatter = metrics.OutputMetricsFormatters
                                                                .OfType<MetricsPrometheusProtobufOutputFormatter>()
                                                                .First();
            });
            services.AddAppMetricsSystemMetricsCollector();
            services.AddMvc().AddMetrics();
        }
    }
}