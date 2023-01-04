using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HolzShots.Net.Custom;

static class JsonConfig
{
    internal static JsonSerializerSettings JsonSettings { get; } = new JsonSerializerSettings
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        Converters = new List<JsonConverter>() {
            new SemVersionConverter()
        }
    };
}
