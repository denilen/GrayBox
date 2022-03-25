namespace Prometheus.Extensions
{
    public static class AspNetCoreMvcBuilderExtensions
    {
        public static void UseAppMvc(this IApplicationBuilder app)
        {
            // app.UseCorrelationId();
            // app.UseMiddleware<RequestCultureMiddleware>();
            app.UseRouting();
            // app.UseGotrgAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}