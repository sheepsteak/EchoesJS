using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Sheepsteak.EchoesJS.Core
{
    public class CTimeToDateTimeOffsetConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            long ticks;

            if (!long.TryParse(reader.Value.ToString(), out ticks))
            {
                throw new Exception(
                    String.Format("Unexpected token parsing date. Expected Integer, got {0}.",
                    reader.TokenType));
            }

            var date = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            date = date.AddSeconds(ticks);

            return date;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }
    }
}
