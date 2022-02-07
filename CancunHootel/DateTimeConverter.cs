using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CancunHootel
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        string _format;

        public DateTimeConverter(string format)
        {
            _format = format;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateTime res = default(DateTime);
            DateTime.TryParseExact(reader.GetString(), _format, null, System.Globalization.DateTimeStyles.None, out res);
            return res;

        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}
