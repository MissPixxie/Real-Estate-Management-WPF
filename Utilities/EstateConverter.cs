using Modern_Real_Estate.Model.EstateTypes;
using Modern_Real_Estate.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Modern_Real_Estate.Utilities
{
    public class EstateConverter : JsonConverter<ObservableList<Estate>>
    {
        public override ObservableList<Estate> ReadJson(JsonReader reader, Type objectType, ObservableList<Estate> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JArray array = JArray.Load(reader);
            ObservableList<Estate> result = new ObservableList<Estate>();

            foreach (JObject item in array)
            {
                string estateType = item["SubType"].Value<string>();
                Estate estate = null;
                switch (estateType)
                {
                    case "Apartment":
                        estate = item.ToObject<Apartment>(serializer);
                        break;
                    case "Townhouse":
                        estate = item.ToObject<Townhouse>(serializer);
                        break;
                    case "Villa":
                        estate = item.ToObject<Villa>(serializer);
                        break;
                    case "Hospital":
                        estate = item.ToObject<Hospital>(serializer);
                        break;
                    case "School":
                        estate = item.ToObject<School>(serializer);
                        break;
                    case "University":
                        estate = item.ToObject<University>(serializer);
                        break;
                    case "Shop":
                        estate = item.ToObject<Shop>(serializer);
                        break;
                    case "Warehouse":
                        estate = item.ToObject<Warehouse>(serializer);
                        break;
                }

                if (estate != null)
                {
                    result.Add(estate);
                }
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, ObservableList<Estate> value, JsonSerializer serializer)
        {
            JArray array = new JArray();

            foreach (var estate in value)
            {
                JObject item = JObject.FromObject(estate, serializer);
                array.Add(item);
            }

            array.WriteTo(writer);
        }
    }
}
