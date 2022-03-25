using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Prometheus.Extensions
{
    public class ValidationDetails
    {
        private Dictionary<string, string[]> Errors { get; }

        public ValidationDetails(ModelStateDictionary modelStateDictionary)
        {
            Errors = new Dictionary<string, string[]>(
                modelStateDictionary
                    .Select(
                        x => KeyValuePair.Create(
                            x.Key,
                            x.Value!.Errors
                             .Select(y => y.ErrorMessage).ToArray())));
        }

        public string Message => "One or more validation errors occurred.";
        public Guid? CorrelationId { get; set; }
        public bool IsSuccess { get; set; }
    }
}