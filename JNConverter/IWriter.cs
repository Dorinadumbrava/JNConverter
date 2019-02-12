using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNConverter
{
    public interface IWriter
    {
        void WriteKvpToFile(string path, IDictionary<string, string> dictionary);
        void WriteJsonToFile(string path, JObject jObject);
    }
}
