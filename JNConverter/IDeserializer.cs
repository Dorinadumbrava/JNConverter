using Newtonsoft.Json.Linq;

namespace JNConverter
{
    public interface IDeserializer
    {
        JToken Deserialize(JProperty prop);
    }
}
