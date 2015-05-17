using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData.Converters
{
    abstract class JsonCreationConverter<T> : JsonConverter
    {
        protected abstract T Create(Type ObjectType, JObject Object);

        public override bool CanConvert(Type ObjectType)
        {
            return typeof(T).IsAssignableFrom(ObjectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject Object = JObject.Load(reader);
            T target = Create(objectType, Object);
            serializer.Populate(Object.CreateReader(), target);

            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
