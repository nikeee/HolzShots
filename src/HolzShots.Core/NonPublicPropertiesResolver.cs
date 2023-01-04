using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HolzShots;

public class NonPublicPropertiesResolver : CamelCasePropertyNamesContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);
        if (member is PropertyInfo pi)
        {
            prop.Readable = (pi.GetMethod is not null);
            prop.Writable = (pi.SetMethod is not null);
        }
        return prop;
    }
}
