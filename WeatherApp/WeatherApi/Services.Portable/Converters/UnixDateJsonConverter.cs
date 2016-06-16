using System;
using Newtonsoft.Json;

namespace Services.Portable.Converters
{
    public class UnixDateJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dateTime = (DateTime)value;
            var defaultDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var resTimeStamp = dateTime.Subtract(defaultDateTime);

            long unixTimeStamp = (long)resTimeStamp.TotalMilliseconds;

            writer.WriteValue(unixTimeStamp);
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.ValueType != typeof(long))
                return new DateTime();

            var unixTimeStamp = (long)reader.Value;

            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            while (unixTimeStamp >= int.MaxValue - 1)
            {
                unixTimeStamp -= int.MaxValue - 1;
                dtDateTime = dtDateTime.AddSeconds(int.MaxValue - 1);
            }

            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime.ToLocalTime();
        }
    }
}