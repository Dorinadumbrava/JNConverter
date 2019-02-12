using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNConverter
{
    public interface IJsonToKvpConverter
    {
        IDictionary<string, string> ParseFromJson(string json);
        IDictionary<string, string> ParseToKvp(JToken input, string name);
    }
}
