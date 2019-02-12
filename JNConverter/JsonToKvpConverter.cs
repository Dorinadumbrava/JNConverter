using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace JNConverter
{
    public class JsonToKvpConverter : IJsonToKvpConverter
    {
        IDeserializer deserializer;

        public JsonToKvpConverter(IDeserializer deserializer)
        {
            this.deserializer = deserializer;
        }
        char valueSeparator = '\t';

        public IDictionary<string, string> ParseFromJson(string json)
        {
            JObject obj = JObject.Parse(json);
            var properties = obj.Properties();
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            IDictionary<string, string> intermediaryValues = new Dictionary<string, string>();
            foreach (var property in properties)
            {
                JToken input = deserializer.Deserialize(property);
                intermediaryValues = ParseToKvp(input, property.Name);
                foreach (KeyValuePair<string, string> keyValuePair in intermediaryValues)
                {
                    dictionary.Add(keyValuePair);
                }
            }
            return dictionary;
        }

        public IDictionary<string, string> ParseToKvp(JToken input, string name)
        {
            IDictionary<string, string> fieldNames = new Dictionary<string, string>();

            if (input != null)
            {
                if (input.GetType() == typeof(JObject))
                {
                    JObject inputJson = JObject.FromObject(input);
                    var result = ParseJObject(inputJson, name);
                    foreach (var item in result)
                    {
                        var subFields = ParseToKvp(item.Value, item.Key);
                        foreach (var row in subFields)
                        {
                            fieldNames.Add(row.Key, row.Value);
                        }
                    }
                }
                else 
                {
                JProperty jProperty = new JProperty(name, input.ToString());
                fieldNames.Add($"\"{jProperty.Name}\"", $"{valueSeparator}{jProperty.Value.ToString()}");
            }
            }
            else
            {
                fieldNames.Add(string.Empty, string.Empty);
            }
            return fieldNames;
        }

        private IDictionary<string, JToken> ParseJObject(JObject inputJson, string name)
        {
            var properties = inputJson.Properties();
            IDictionary<string, JToken> result = new Dictionary<string, JToken>();
            foreach (var property in properties)
            {
                var pName = $"{name}\".\"{property.Name}";
                result.Add(pName, property.Value);
            }
            return result;
        }
    }
}


