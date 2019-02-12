using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JNConverter
{
    public class Writer : IWriter
    {
        public void WriteKvpToFile(string path, IDictionary<string, string> dictionary)
        {
            try
            {
                File.WriteAllLines(path,
                    dictionary.Select(x => x.Key + x.Value).ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void WriteJsonToFile(string path, JObject jObject)
        {
            try
            {
                File.WriteAllText(path, jObject.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
