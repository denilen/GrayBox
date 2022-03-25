using System.Reflection;
using FluentValidation.AspNetCore;
using Gotrg.Common.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.Extensions.DependencyInjection;

namespace Prometheus.Extensions
{
    public static class AspNetCoreMvcServiceExtensions
    {
        public static void AddAppMvc(this IServiceCollection services)
        {
            services.AddAppMvc(false);
        }

        public static void AddAppMvc(this IServiceCollection services,
                                     bool allowAnonymousFilter,
                                     bool? isSecure = null)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory =
                    (context) =>
                    {
                        LogActionContext(context);
                        var correlationProvider = context.HttpContext
                                                         .RequestServices
                                                         .GetRequiredService<ICorrelationProvider>();

                        return new BadRequestObjectResult(new ValidationDetails(context.ModelState)
                        {
                            CorrelationId = correlationProvider.CorrelationId
                        });
                    };
            });

            services.AddControllers(
                        opts =>
                        {
                            if (allowAnonymousFilter)
                                opts.Filters.Add(new AllowAnonymousFilter());
                        })
                    .AddFluentValidation()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings
                               .Converters
                               .Add(new StringEnumConverter());
                    });
        }

        private static void LogActionContext(ActionContext context)
        {
            if (context is ActionExecutingContext executingContext)
                LogActionExecutingContext(executingContext);

            var logger = context.HttpContext
                                .RequestServices
                                .GetRequiredService<ILogger<ValidationDetails>>();

            try
            {
                var modelStateJson = JsonConvert.SerializeObject(new ValidationDetails(context.ModelState));
                logger.LogWarning("Bad Model State: '{ModelStateJson}'", modelStateJson);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Can't log ModelState");
            }
        }

        private static void LogActionExecutingContext(ActionExecutingContext context)
        {
            var loggingFilter = context.Controller
                                       .GetType()
                                       .GetCustomAttribute<LoggingActionFilterAttribute>();

            loggingFilter?.OnActionExecuting(context);
        }
    }
}