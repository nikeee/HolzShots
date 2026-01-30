using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HolzShots.Net.Custom;

static class JsonConfig
{
    internal static JsonSerializerOptions JsonOptions { get; } = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new SemVersionConverter() },
        PropertyNameCaseInsensitive = true,
        WriteIndented = false,
    };
}
