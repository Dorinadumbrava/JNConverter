using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNConverter
{
    public interface IPipeline
    {
        void Serialize(string inPath, string outPath);
        void Deserialize(string inPath, string outPath);
    }
}
