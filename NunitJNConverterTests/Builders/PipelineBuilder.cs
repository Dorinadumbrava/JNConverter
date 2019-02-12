using JNConverter;
using NSubstitute;

namespace NunitJNConverterTests.Builders
{
    class PipelineBuilder
    {
        private IWriter writer;
        private IReader reader;
        private IJsonToKvpConverter jsonDeserializer;
        private IKvpToJsonConverter jsonSerializer;

        public PipelineBuilder()
        {
            this.writer = Substitute.For<IWriter>();
            this.reader = Substitute.For<IReader>();
            this.jsonSerializer = Substitute.For<IKvpToJsonConverter>();
            this.jsonDeserializer = Substitute.For<IJsonToKvpConverter>();
        }

        public PipelineBuilder WithWriter (IWriter writer)
        {
            this.writer = writer;
            return this;
        }

        public PipelineBuilder WithReader(IReader reader)
        {
            this.reader = reader;
            return this;
        }

        public PipelineBuilder WithJsonToKvpConverter(IJsonToKvpConverter jsonDeserializer)
        {
            this.jsonDeserializer = jsonDeserializer;
            return this;
        }

        public PipelineBuilder WithKvpToJsonConverter(IKvpToJsonConverter jsonSerializer)
        {
            this.jsonSerializer = jsonSerializer;
            return this;
        }

        public Pipeline Build()
        {
            return new Pipeline(writer, reader, jsonDeserializer, jsonSerializer);
        }
    }
}
