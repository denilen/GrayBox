using System.Reflection;
using Facility.HttpClient;
using Microsoft.Extensions.Options;

namespace Facility.Here.ClientOptions
{
    public class ServiceClientConfigureOptions<T> : IConfigureOptions<T> where T : BaseClientOptions
    {
        public virtual void Configure(T options)
        {
            options.Application = Assembly.GetEntryAssembly()?.GetName().Name;
            options.ApplicationVersion = Assembly.GetEntryAssembly()?.GetName().Version.ToString();
        }
    }
}