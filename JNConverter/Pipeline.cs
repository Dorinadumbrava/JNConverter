namespace JNConverter
{
    public class Pipeline : IPipeline
    {
        private IWriter writer;
        private IReader reader;
        private IJsonToKvpConverter jsonDeserializer;
        private IKvpToJsonConverter jsonSerializer;

        public Pipeline(IWriter writer, IReader reader, IJsonToKvpConverter jsonDeserializer, IKvpToJsonConverter jsonSerializer)
        {
            this.writer = writer;
            this.reader = reader;
            this.jsonSerializer = jsonSerializer;
            this.jsonDeserializer = jsonDeserializer;
        }

        public void Deserialize(string inPath, string outPath)
        {
            var json = reader.ReadFromJsonFile(inPath);
            var dictionary = jsonDeserializer.ParseFromJson(json);
            writer.WriteKvpToFile(outPath, dictionary);
        }

        public void Serialize(string inPath, string outPath)
        {
            var lines = reader.ReadFromTextFile(inPath);
            var json = jsonSerializer.ParseToJson(lines);
            writer.WriteJsonToFile(outPath, json);
        }
    }
}
