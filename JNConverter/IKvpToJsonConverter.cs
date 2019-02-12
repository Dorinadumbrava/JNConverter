using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNConverter
{
    public interface IKvpToJsonConverter
    {
        JObject ParseToJson(string[] input);
        JObject ParseFromKvp(string key, string value);
    }
}
