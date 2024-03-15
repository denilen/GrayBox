using System.Collections.Generic;

namespace Facility.HttpClient
{
    public interface ICustomDictionary: IEnumerable<KeyValuePair<string, string>>
    {
        void Add(string key, string value);
    }
}
