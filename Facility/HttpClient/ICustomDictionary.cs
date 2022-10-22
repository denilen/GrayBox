using System.Collections.Generic;

namespace ConsoleApp.HttpClient
{
    public interface ICustomDictionary: IEnumerable<KeyValuePair<string, string>>
    {
        void Add(string key, string value);
    }
}
