using System;
using System.Collections.Generic;

namespace ConsoleApp.HttpClient
{
    public abstract class BaseClientOptions
    {
        public Uri BaseUrl { get; set; }
        public string Application { get; set; }
        public string ApplicationVersion { get; set; }
        public int Timeout { get; set; } = 40;
        public Dictionary<string, CallOption> CallOptions { get; set; }
    }
}
