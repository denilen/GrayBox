using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Prometheus.Extensions
{
    public class LoggingActionFilterAttribute : Attribute, IActionFilter
    {
        private const int MaxBodyLength = 10000;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var genericType = typeof(ILogger<>).MakeGenericType(context.Controller.GetType());
            var logger = (ILogger)context.HttpContext.RequestServices.GetService(genericType)!;

            try
            {
                var httpContentSecurityFilter = context.HttpContext.RequestServices.GetService<HttpContentSecurityFilter>();
                context.HttpContext.Items["StartedOn"] = DateTimeOffset.UtcNow;

                var request = context.HttpContext?.Request;
                var httpMethod = $"{request?.Method}";
                var actionMethod = $"{(context.ActionDescriptor as ControllerActionDescriptor)?.ActionName}";
                var isFormFile = context.ActionArguments?.Select(x => x.Value?.GetType()).Contains(typeof(FormFile));
                var actionArguments = "[" + string.Join(",", context.ActionArguments?.Select(x => $"{x.Key}={x.Value}").ToArray() ?? Array.Empty<string>()) + "]";
                string queryString = request?.QueryString.ToString();
                string body = "";
                if (request != null && isFormFile != true)
                {
                    StreamReader sr = new StreamReader(request.Body);
                    if (sr.BaseStream.CanSeek)
                    {
                        sr.BaseStream.Position = 0;
                    }

                    body = sr.ReadToEndAsync().GetAwaiter().GetResult();
                    body = httpContentSecurityFilter.UpdateContent(body);
                    if (body?.Length > MaxBodyLength)
                    {
                        body = body.Substring(0, MaxBodyLength) + " ...";
                    }
                }

                var result = "Executing:\tHttpMethod={httpMethod} ActionMethod={actionMethod} ActionArguments={actionArguments} QueryString={queryString} Body={body}";
                logger.LogInformation(result, httpMethod, actionMethod, actionArguments, queryString, body);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Internal logger Error in OnActionExecuting. Not possible to log.");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var genericType = typeof(ILogger<>).MakeGenericType(context.Controller.GetType());
            var logger = (ILogger) context.HttpContext.RequestServices.GetService(genericType);

            try
            {
                var request = context.HttpContext?.Request;
                var httpMethod = $"{request?.Method}";
                var actionMethod = $"{(context.ActionDescriptor as ControllerActionDescriptor)?.ActionName}";

                var dtStartedOn = (DateTimeOffset?)context.HttpContext.Items["StartedOn"];
                var elapsedTime = 0;
                if (dtStartedOn != null)
                    elapsedTime = (int)(DateTimeOffset.UtcNow - dtStartedOn.Value).TotalMilliseconds;

                logger.LogInformation("Executed:\tHttpMethod={httpMethod} ActionMethod={actionMethod} ElapsedTime={elapsed}", httpMethod, actionMethod, elapsedTime);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Internal logger Error in OnActionExecuted. Not possible to log.");
            }
        }
    }
}
