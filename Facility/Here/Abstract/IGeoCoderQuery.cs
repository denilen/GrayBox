using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp.Here.Model;

namespace ConsoleApp.Here.Abstract
{
    public interface IGeoCoderQuery
    {
        Task<List<GeoCoderModel>> ExecuteAsync(string postalCode,
                                               string city,
                                               string address,
                                               string countryCode,
                                               int responseListLimit,
                                               CancellationToken cancellationToken = new CancellationToken());
    }
}