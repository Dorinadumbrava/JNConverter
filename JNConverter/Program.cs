using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JNConverter
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var converter = kernel.Get<IPipeline>();
            string inPath = args[0];
            string outPath = args[1];

            if (Path.GetExtension(inPath) == ".json")
            {
                converter.Deserialize(inPath, outPath);
            }
            else
            {
                converter.Serialize(outPath, inPath);
            }
        }
    }
}
