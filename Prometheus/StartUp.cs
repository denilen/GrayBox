using AutoMapper;
using Prometheus.Extensions;

namespace Prometheus;

public class StartUp
{
    public StartUp(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAppMetrics(Configuration);
        services.AddAppMvc();
    }

    public void Configure(IApplicationBuilder app,
                          IWebHostEnvironment env,
                          ILogger<StartUp> logger)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAppMetrics();
        app.UseAppMvc();
    }
}