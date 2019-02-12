using Ninject.Modules;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNConverter
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IWriter>().To<Writer>();
            Bind<IReader>().To<Reader>();
            Bind<IKvpToJsonConverter>().To<KvpToJsonConverter>();
            Bind<IJsonToKvpConverter>().To<JsonToKvpConverter>();
            Bind<IPipeline>().To<Pipeline>();
            Bind<IDeserializer>().To<Deserializer>();
        }
    }
}
