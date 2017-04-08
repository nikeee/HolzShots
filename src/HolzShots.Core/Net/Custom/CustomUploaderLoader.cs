using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HolzShots.Net.Custom
{
    class CustomUploaderLoader
    {
        private const string SupportedSchema = "0.1.0";
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static bool TryLoad(string value, out CustomUploader result)
        {
            result = null;
            if (string.IsNullOrWhiteSpace(value))
                return false;
            result = JsonConvert.DeserializeObject<CustomUploader>(value, JsonSettings);
            return result?.Validate(SupportedSchema) == true;
        }
    }
}
