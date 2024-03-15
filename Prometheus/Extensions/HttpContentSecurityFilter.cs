using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NJsonSchema;
using NJsonSchema.Generation;

namespace Prometheus.Extensions
{
    public class HttpContentSecurityFilter
    {
        private static bool                       _enabled;
        private static HttpContentSecurityFilter? CurrentInstance { get; set; }
        private        List<Type>                 _typeModels;

        private HttpContentSecurityFilter()
        {
        }

        public static HttpContentSecurityFilter GetInstance(bool? enabled = null)
        {
            var instance = CurrentInstance ?? new HttpContentSecurityFilter();
            if (enabled.HasValue)
                _enabled = enabled.Value;
            return instance;
        }

        private void Init()
        {
            if (_typeModels != null)
                return;
            _typeModels = new List<Type>();
            if (!_enabled)
                return;

            _typeModels =
                AppDomain.CurrentDomain.GetAssemblies()
                         .SelectMany(t => t.GetTypes())
                         .Where(x =>
                             x.CustomAttributes
                              .Any(a => a.AttributeType == typeof(SecureContentAttribute)))
                         .ToList();
        }

        public string UpdateContent(string content)
        {
            Init();
            
            foreach (var securityType in _typeModels)
            {
                var schema =
                    JsonSchema.FromType(securityType, new JsonSchemaGeneratorSettings()
                    {
                        SerializerSettings =
                            new JsonSerializerSettings()
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                Converters = { new StringEnumConverter() }
                            }
                    });
                try
                {
                    if (schema.Validate(content)?.Count == 0)
                        return "# Content is hidden due to security policy #";
                }
                catch
                {
                }
            }

            return content;
        }
    }
}