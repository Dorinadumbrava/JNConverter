using JNConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using NSubstitute;
using NunitJNConverterTests.Builders;
using System.Collections.Generic;

namespace NunitJNConverterTests
{
    [TestClass]
    public class PipelineTests
    {
        [TestMethod]
        public void DeserializeCallsReader()
        {
            string path = "";
            string outPath = "dd";
            var reader = Substitute.For<IReader>();
            reader.ReadFromJsonFile(path).Returns("result");

            var pipeline = new PipelineBuilder().WithReader(reader).Build();

            pipeline.Deserialize(path, outPath);

            reader.Received().ReadFromJsonFile(path);
        }

        [TestMethod]
        public void DeserializeCallsJsonToKvpConverter()
        {
            string path = "";
            string outPath = "dd";
            string json = "";
            var jsonDeserializer = Substitute.For<IJsonToKvpConverter>();
            jsonDeserializer.ParseFromJson(json);

            var pipeline = new PipelineBuilder().WithJsonToKvpConverter(jsonDeserializer).Build();

            pipeline.Deserialize(path, outPath);

            jsonDeserializer.Received().ParseFromJson(json);
        }

        [TestMethod]
        public void DeserializeCallsWriter()
        {
            string path = "";
            string outPath = "dd";
            var dictionary = new Dictionary<string, string>();
            var writer = Substitute.For<IWriter>();
            writer.WriteKvpToFile(path, dictionary);

            var pipeline = new PipelineBuilder().WithWriter(writer).Build();

            pipeline.Deserialize(path, outPath);

            writer.Received().WriteKvpToFile(path, dictionary);
        }

        [TestMethod]
        public void SerializeCallsReader()
        {
            string path = "";
            string outPath = "dd";
            var reader = Substitute.For<IReader>();
            reader.ReadFromTextFile(path).Returns(new string[1]);

            var pipeline = new PipelineBuilder().WithReader(reader).Build();

            pipeline.Serialize(path, outPath);

            reader.Received().ReadFromTextFile(path);
        }

        [TestMethod]
        public void SerializeCallsJsonToKvpConverter()
        {
            string path = "";
            string outPath = "dd";
            string[] lines = { "", "d"};
            var jsonSerializer = Substitute.For<IKvpToJsonConverter>();
            jsonSerializer.ParseToJson(lines);

            var pipeline = new PipelineBuilder().WithKvpToJsonConverter(jsonSerializer).Build();

            pipeline.Serialize(path, outPath);

            jsonSerializer.Received().ParseToJson(lines);
        }

        [TestMethod]
        public void SerializeCallsWriter()
        {
            string path = "";
            string outPath = "dd";
            var jObject = new JObject();
            var writer = Substitute.For<IWriter>();
            writer.WriteJsonToFile(outPath, jObject);

            var pipeline = new PipelineBuilder().WithWriter(writer).Build();

            pipeline.Deserialize(path, outPath);

            writer.Received().WriteJsonToFile(outPath, jObject);
        }
    }
}
