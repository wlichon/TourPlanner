using System.Formats.Asn1;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Globalization;
using System.Text.Json;

namespace JsonTools
{
    /// <summary>
    /// TimeSpans are not serialized consistently depending on what properties are present. So this 
    /// serializer will ensure the format is maintained no matter what.
    /// </summary>
    /// 

    public class TimespanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader == null || reader.Value == null)
            {
                return TimeSpan.MinValue;
            }
            else
            {
                var r = reader.Value;
                var s = r.ToString();
                return TimeSpan.Parse(s);
            }
        }

        public override void WriteJson(JsonWriter writer, TimeSpan value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}