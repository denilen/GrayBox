using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Facility.Here.Abstract;
using Facility.Here.ClientOptions;
using Facility.HttpClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Facility.Here
{
    public class HereGeoCoderServiceClient : BaseServiceClient<HereOptions>, IHereGeoCoderServiceClient
    {
        private readonly IMapper _mapper;

        public HereGeoCoderServiceClient(IHttpClientFactory clientFactory,
                                         IOptions<HereOptions> clientOptions,
                                         ILogger<HereGeoCoderServiceClient> logger,
                                         IMapper mapper)
            : base(clientFactory, clientOptions.Value, logger)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Request for coordinates by address from here.com
        /// </summary>
        /// <param name="address"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-autosuggest-brief.html</remarks>
        public async Task<List<HereGeoCoderDto>> GeoCoder(string address, CancellationToken cancellationToken = default)
        {
            var jResult = await GetAsync<JObject>("v1/geocode", new Dictionary<string, string>
            {
                { "q", address },
                { "apiKey", ClientOptions.ApiKey }
            }, null, cancellationToken);

            var result = jResult.Value<JArray>("items").ToObject<List<HereGeoCoderDto>>();

            return result;
        }

        /// <summary>
        /// Request AutoSuggest objects by address and coordinates from here.com
        /// </summary>
        /// <param name="address"></param>
        /// /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<HereGeoCoderDto>> AddressInterpolation(string address,
                                                                      double lat,
                                                                      double lon,
                                                                      CancellationToken cancellationToken = default)
        {
            var jResult = await GetAsync<JObject>("v1/autosuggest", new Dictionary<string, string>
            {
                { "at", string.Join(",", lat, lon) },
                { "lang", "en" },
                { "q", address },
                { "apiKey", ClientOptions.ApiKey }
            }, null, cancellationToken);

            var result = jResult.Value<JArray>("items").ToObject<List<HereGeoCoderDto>>();

            return result;
        }
    }
}