using FluentAssertions;
using JNConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSubstitute;
using NunitJNConverterTests.Builders;
using System.Collections.Generic;

namespace NunitJNConverterTests
{
    [TestClass]
    public class JsonToKvpConverterTests
    {
        [TestMethod]
        public void ParseFromJsonReturnsCorrectDictionaryForValidString()
        {
            string json = "{\"A\": 123}";
            JProperty jProperty = new JProperty("A", 123);
            var dictionary = new Dictionary<string, string>();
            dictionary.Add($"\"{"A"}\"", $"\t{123}");
            JToken token = JsonConvert.DeserializeObject<JToken>(jProperty.Value.ToString());

            var deserializer = Substitute.For<IDeserializer>();
            deserializer.Deserialize(Arg.Any<JProperty>()).Returns(token);
            var converter = new JsonToKvpConverterBuilder().WithDeserializer(deserializer).Build();
            var result = converter.ParseFromJson(json);

            result.Should().BeEquivalentTo(dictionary);
        }

        [TestMethod]
        public void ParseFromJsonReturnsDictionaryForValidString()
        {
            string json = "{\"A\": 123}";
            JProperty jProperty = new JProperty("A", 123);
            JToken token = JsonConvert.DeserializeObject<JToken>(jProperty.Value.ToString());

            var deserializer = Substitute.For<IDeserializer>();
            deserializer.Deserialize(Arg.Any<JProperty>()).Returns(token);
            var converter = new JsonToKvpConverterBuilder().WithDeserializer(deserializer).Build();
            var result = converter.ParseFromJson(json);

            result.Should().BeOfType<Dictionary<string, string>>();
        }

        [TestMethod]
        public void ParseToKvpReturnsEmptyDictionaryForEmptyInput()
        {
            JToken input = null;
            string name = "A";
            IDictionary<string, string> fieldNames = new Dictionary<string, string>();
            fieldNames.Add(string.Empty, string.Empty);

            var converter = new JsonToKvpConverterBuilder().Build();
            var result = converter.ParseToKvp(input, name);

            result.Should().BeEquivalentTo(fieldNames);
        }

        [TestMethod]
        public void ParseToKvpReturnsDictionaryForJObjectInput()
        {
            JToken input = JsonConvert.DeserializeObject<JToken>("{\r\n  \"B\": {\r\n    \"C\": 123\r\n  }\r\n}");
            string name = "A";

            var converter = new JsonToKvpConverterBuilder().Build();
            var result = converter.ParseToKvp(input, name);

            result.Should().BeOfType<Dictionary<string, string>>();
        }


        [TestMethod]
        public void ParseToKvpReturnsCorrectDictionaryForJObjectInput()
        {
            JToken input = JsonConvert.DeserializeObject<JToken>("{\r\n  \"B\": {\r\n    \"C\": 123\r\n  }\r\n}");
            string name = "A";
            IDictionary<string, string> fieldNames = new Dictionary<string, string>();
            string propName = $"\"{"A"}\".\"{"B"}\".\"{"C"}\"";
            string propValue = $"\t{123}";
            fieldNames.Add($"\"{"A"}\".\"{"B"}\".\"{"C"}\"", $"\t{123}");

            var converter = new JsonToKvpConverterBuilder().Build();
            var result = converter.ParseToKvp(input, name);

            result.Should().BeEquivalentTo(fieldNames);
        }

        [TestMethod]
        public void ParseToKvpReturnsDictionaryForJPropertyImput()
        {
            JToken input = JsonConvert.DeserializeObject<JToken>("123");
            string name = "A";

            var converter = new JsonToKvpConverterBuilder().Build();
            var result = converter.ParseToKvp(input, name);

            result.Should().BeOfType<Dictionary<string, string>>();
        }

        [TestMethod]
        public void ParseToKvpReturnsCorrectDictionaryForJPropertyImput()
        {
            JToken input = JsonConvert.DeserializeObject<JToken>("123");
            string name = "A";
            IDictionary<string, string> fieldNames = new Dictionary<string, string>();
            fieldNames.Add($"\"{"A"}\"", $"\t{123}");

            var converter = new JsonToKvpConverterBuilder().Build();
            var result = converter.ParseToKvp(input, name);

            result.Should().BeEquivalentTo(fieldNames);
        }

        [TestMethod]
        public void ParseToKvpReturnsCorrectDictionaryForMultipleObjectInput()
        {
            JToken input = 
                JsonConvert.DeserializeObject<JToken>("{\r\n  \"B\": {\r\n    \"C\": 123\r\n  },\r\n  \"E\": {\r\n    \"F\": 456\r\n  }\r\n}");
            string name = "A";
            IDictionary<string, string> fieldNames = new Dictionary<string, string>();
            fieldNames.Add($"\"{"A"}\".\"{"B"}\".\"{"C"}\"", $"\t{123}");
            fieldNames.Add($"\"{"A"}\".\"{"E"}\".\"{"F"}\"", $"\t{456}");

            var converter = new JsonToKvpConverterBuilder().Build();
            var result = converter.ParseToKvp(input, name);

            result.Should().BeEquivalentTo(fieldNames);
        }

        [TestMethod]
        public void ParseToKvpReturnsFullDictionaryForMultipleObjectInput()
        {
            JToken input =
                JsonConvert.DeserializeObject<JToken>("{\r\n  \"B\": {\r\n    \"C\": 123\r\n  },\r\n  \"E\": {\r\n    \"F\": 456\r\n  }\r\n}");
            string name = "A";
            IDictionary<string, string> fieldNames = new Dictionary<string, string>();
            fieldNames.Add($"\"{"A"}\".\"{"B"}\".\"{"C"}\"", $"\t{123}");
            fieldNames.Add($"\"{"A"}\".\"{"E"}\".\"{"F"}\"", $"\t{456}");

            var converter = new JsonToKvpConverterBuilder().Build();
            var result = converter.ParseToKvp(input, name);

            result.Count.Should().Be(2);
        }
    }
}
    