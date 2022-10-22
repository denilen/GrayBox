using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp.Here.Model;

namespace ConsoleApp.Here.Abstract
{
    public interface ICountriesQuery
    {
        Task<List<CountryModel>> ExecuteAsync(CancellationToken cancellationToken = new CancellationToken());
        string UiCountryCodeConvert(string countryCode);
    }
}