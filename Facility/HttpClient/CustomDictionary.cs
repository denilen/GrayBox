using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ConsoleApp.HttpClient
{
    public class CustomDictionary : ICustomDictionary
    {
        private ConcurrentDictionary<string, string> _dictionary = new ConcurrentDictionary<string, string>();

        public void Add(string key, string value)
        {
            _dictionary.TryAdd(key, value);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _dictionary).GetEnumerator();
        }
    }
}
