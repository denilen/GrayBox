using System.Net;
using System.Net.Http;

namespace ConsoleApp.HttpClient.Exceptions
{
    public class HttpResponseException : BaseException
    {
        private readonly string _message;
        public HttpResponseMessage HttpResponseMessage { get; }
        public string Body { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public HttpResponseException(HttpResponseMessage httpResponseMessage)
        {
            var message = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Body = message;
            StatusCode = httpResponseMessage.StatusCode;
            HttpResponseMessage = httpResponseMessage;
            _message = $"StatusCode = '{httpResponseMessage.StatusCode}'. Body = '{message}'";
        }

        public override string Message => _message;
    }
}
