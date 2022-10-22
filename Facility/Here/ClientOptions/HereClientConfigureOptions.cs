using Microsoft.Extensions.Configuration;

namespace ConsoleApp.Here.ClientOptions
{
    public class HereClientConfigureOptions : ServiceClientConfigureOptions<HereOptions>
    {
        private readonly IConfiguration _configuration;

        public HereClientConfigureOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void Configure(HereOptions options)
        {
            base.Configure(options);
            _configuration.GetSection("HereClient").Bind(options);
        }
    }
}