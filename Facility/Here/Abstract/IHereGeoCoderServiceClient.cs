using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Facility.Here.Abstract
{
    public interface IHereGeoCoderServiceClient
    {
        Task<List<HereGeoCoderDto>> GeoCoder(string address,
                                             CancellationToken cancellationToken = default);

        Task<List<HereGeoCoderDto>> AddressInterpolation(string address,
                                                         double lat,
                                                         double lon,
                                                         CancellationToken cancellationToken = default);
    }
}