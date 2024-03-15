using Microsoft.Extensions.Configuration;

namespace Facility.Here.ClientOptions
{
    public class HereClientConfigureOptions : ServiceClientConfigureOptions<HereOptions>
    {
        private readonly IConfiguration _configuration;

        public HereClientConfigureOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // public override void Configure(HereOptions options)
        // {
        //     base.Configure(options);
        //     _configuration.GetSection("HereClient").Bind(options);
        // }
    }
}