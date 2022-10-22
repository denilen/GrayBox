namespace ConsoleApp.HttpClient.Exceptions
{
    public class HttpTransferException : BaseException
    {
        public HttpResponseException HttpResponseException { get; }

        public HttpTransferException(HttpResponseException httpResponseException)
            : base("Transfer: " + httpResponseException.Body)
        {
            HttpResponseException = httpResponseException;
        }
    }
}
