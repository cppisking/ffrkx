using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData.Converters
{
    class EpochToDateTime : JsonConverter
    {
        private static readonly DateTime mEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            ulong unix_time = Convert.ToUInt64(reader.Value);

            DateTime result = mEpoch.AddSeconds(unix_time);
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DateTime result = (DateTime)value;
            TimeSpan delta = result.Subtract(mEpoch);
            writer.WriteComment(delta.TotalSeconds.ToString());
        }
    }
}
