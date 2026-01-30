using System.Text.Json;
using System.Text.Json.Serialization;
using Semver;

namespace HolzShots.Net.Custom;

/// <summary>
/// Taken from
/// https://gist.github.com/madaz/efab4a5554b88dc2862d58046ddba00f
/// (https://github.com/maxhauser/semver/issues/21)
/// Migrated to System.Text.Json
/// </summary>
public class SemVersionConverter : JsonConverter<SemVersion>
{
    public override void Write(Utf8JsonWriter writer, SemVersion value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(writer);

        if (value == null)
        {
            writer.WriteNullValue();
        }
        else
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    public override SemVersion? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException($"Unexpected token or value when parsing version. Token: {reader.TokenType}");

        try
        {
            var v = reader.GetString();
            return SemVersion.Parse(v, SemVersionStyles.Strict);
        }
        catch (Exception ex)
        {
            throw new JsonException($"Error parsing SemVersion string: {reader.GetString()}", ex);
        }
    }
}
