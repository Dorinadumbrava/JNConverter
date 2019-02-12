using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNConverter
{
    public class Reader : IReader
    {
        public string ReadFromJsonFile(string path)
        {
            string json;
            using (StreamReader streamReader = new StreamReader(path))
            {
                try
                {
                    json = streamReader.ReadToEnd();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return json;
        }

        public string[] ReadFromTextFile(string path)
        {
            string[] kvpLines;
            try
            {
                kvpLines = File.ReadAllLines(path);
            }
            catch (Exception)
            {

                throw;
            }
            return kvpLines;
        }
    }
}
