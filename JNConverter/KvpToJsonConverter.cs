using Newtonsoft.Json.Linq;
using System.IO;

namespace JNConverter
{
    public class KvpToJsonConverter : IKvpToJsonConverter
    {
        char valueSeparator = '\t';

        public JObject ParseToJson(string[] input)
        {
            var jObject = new JObject();
            foreach (var line in input)
            {
                var vline = line.Replace("\"", "");
                string[] pathValue = vline.Split(valueSeparator);
                jObject.Merge(ParseFromKvp(pathValue[0], pathValue[1]));
            }
            return jObject;
        }

        public JObject ParseFromKvp(string key, string value)
        {
            JObject jObj = new JObject();
            if (string.IsNullOrEmpty(key))
            {
                return jObj;
            }
            int i = key.IndexOf('.');
            if (i > -1)
            {
                jObj.Add(key.Substring(0, i), ParseFromKvp(key.Substring(i + 1), value));
            }
            else
            {
                jObj.Add(key, value);
            }
            return jObj;
        }
    }
}
