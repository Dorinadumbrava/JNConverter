using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNConverter
{
    class Deserializer : IDeserializer
    {
        public JToken Deserialize(JProperty prop)
        {
            string token = prop.Value.ToString();
            JToken input;
            try
            {
                input = JsonConvert.DeserializeObject<JToken>(token);
            }
            catch (System.Exception)
            {
                throw;
            }
            return input.Root ?? input.First ?? input;
        }
    }
}
