using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Jsonizer
{
    public class MyJsonizer : IJsonizer
    {

        private JsonSerializerOptions jsonSerializerOptions { get; init; }
        public MyJsonizer(bool propertyNameCaseInsensitive = false, JavaScriptEncoder? encoder = default,
            JsonNamingPolicy? propertyNamingPolicy = default, ReferenceHandler? referenceHandler = default)
        {

            jsonSerializerOptions = new()
            {
                PropertyNameCaseInsensitive = propertyNameCaseInsensitive,
                Encoder = encoder ?? JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = propertyNamingPolicy ?? JsonNamingPolicy.CamelCase,
                ReferenceHandler = referenceHandler ?? ReferenceHandler.IgnoreCycles
            };


            //jsonSerializerOptions = new()
            //{
            //    PropertyNameCaseInsensitive = false, /// Gets or sets a value that determines whether a property's name uses a case-insensitive comparison during deserialization. The default value is false.
            //    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, ///   Gets a built-in JavaScript encoder instance that is less strict about what is encoded.
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  /// first  char of properties will be camel (small) case
            //    ReferenceHandler = ReferenceHandler.IgnoreCycles ///  this is important, for meny to many releation we must ignore cycles
            //};
        }

        public string JsonizerToString<T>(T obj)
        {
            using MemoryStream stream = new();
            JsonSerializer.Serialize(stream, obj, jsonSerializerOptions);
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();

        }

        public async Task<string> JsonizerToStringAsync<T>(T obj)
        {
            using MemoryStream stream = new();
            await JsonSerializer.SerializeAsync(stream, obj, jsonSerializerOptions);
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();

        }

        public T? JsonizertoObject<T>(string jsonString)
        {
            using MemoryStream stream = new();
            using StreamWriter streamWriter = new(stream);
            streamWriter.Write(jsonString);
            streamWriter.Flush();
            stream.Position = 0;
            return JsonSerializer.Deserialize<T>(stream, jsonSerializerOptions);
        }

        public async Task<T?> JsonizertoObjectAsync<T>(string jsonString)
        {
            using MemoryStream stream = new();
            using StreamWriter streamWriter = new(stream);
            await streamWriter.WriteAsync(jsonString);
            streamWriter.Flush();
            stream.Position = 0;
            return await JsonSerializer.DeserializeAsync<T>(stream, jsonSerializerOptions);
        }

       
    }
}
