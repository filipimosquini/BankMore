using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Transfer.Infrastructure.CrossCutting.Extensions;

public static class JsonExtenisons
{
    public static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        DateFormatHandling = DateFormatHandling.IsoDateFormat,
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        MissingMemberHandling = MissingMemberHandling.Ignore,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        TypeNameHandling = TypeNameHandling.None,
        Converters = (IList<JsonConverter>)new JsonConverter[1]
        {
            (JsonConverter)new StringEnumConverter()
        }
    };

    public static T FromJson<T>(this string json)
        => string.IsNullOrWhiteSpace(json) ? default : JsonConvert.DeserializeObject<T>(json);


    public static T FromJson<T>(this string json, JsonSerializerSettings jsonSerializerSettings)
        => string.IsNullOrWhiteSpace(json) ? default :
            jsonSerializerSettings != null ?
                JsonConvert.DeserializeObject<T>(json, jsonSerializerSettings)
                : throw new ArgumentNullException(nameof(jsonSerializerSettings));

    public static string ToJson(this object source)
        => source == null
            ? (string)null
            : JsonConvert.SerializeObject(source, Formatting.Indented, JsonSerializerSettings);

    public static bool IsValidJson(this string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return false;
        }

        json = json.Trim();

        if ((json.StartsWith("{") && json.EndsWith("}")) ||
            (json.StartsWith("[") && json.EndsWith("]")))
        {
            try
            {
                var obj = JToken.Parse(json);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}