using JNConverter;
using NSubstitute;

namespace NunitJNConverterTests.Builders
{
    public class JsonToKvpConverterBuilder
    {
        private IDeserializer deserializer;

        public JsonToKvpConverterBuilder()
        {
            deserializer = Substitute.For<IDeserializer>();
        }

        public JsonToKvpConverterBuilder WithDeserializer( IDeserializer deserializer)
        {
            this.deserializer = deserializer;
            return this;
        }

        public JsonToKvpConverter Build()
        {
            return new JsonToKvpConverter(deserializer);
        }
    }
}
