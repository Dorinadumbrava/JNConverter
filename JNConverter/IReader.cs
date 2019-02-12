using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNConverter
{
    public interface IReader
    {
        string ReadFromJsonFile(string path);
        string[] ReadFromTextFile(string path);
    }
}
