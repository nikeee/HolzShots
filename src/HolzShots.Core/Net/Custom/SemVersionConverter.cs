using System;
using Newtonsoft.Json;
using Semver;

namespace HolzShots.Net.Custom
{
    /// <summary>
    /// Taken from
    /// https://gist.github.com/madaz/efab4a5554b88dc2862d58046ddba00f
    /// (https://github.com/maxhauser/semver/issues/21)
    /// </summary>
    public class SemVersionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                if (!(value is SemVersion))
                    throw new JsonSerializationException("Expected SemVersion object value");
                writer.WriteValue(value.ToString());
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            if (reader.TokenType == JsonToken.Null)
                return null;
            if (reader.TokenType != JsonToken.String)
                throw new JsonSerializationException($"Unexpected token or value when parsing version. Token: {reader.TokenType}, Value: {reader.Value}");
            try
            {
                return SemVersion.Parse((string)reader.Value);
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException($"Error parsing SemVersion string: {reader.Value}", ex);
            }
        }

        public override bool CanConvert(Type objectType) => objectType == typeof(SemVersion);
    }
}
