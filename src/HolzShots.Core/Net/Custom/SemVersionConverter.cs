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
        var value = reader.GetString();
        if (value is null) // value is null if the token is a null token
            return null;

        try
        {
            return SemVersion.Parse(value, SemVersionStyles.Strict);
        }
        catch (Exception ex)
        {
            throw new JsonException($"Error parsing SemVersion string: {reader.GetString()}", ex);
        }
    }
}
