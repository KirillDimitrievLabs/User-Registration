using System;
using System.IO;
using UserRegistration.Models;
using YamlDotNet.Serialization;

namespace UserRegistration.Components.Core
{
    class YamlConnection : ISource
    {
        private string Path { get; set; }
        public YamlConnection(string path)
        {
            Path = path;
        }

        public UserSourceModel[] Read()
        {
            IDeserializer deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .Build();

            return deserializer.Deserialize<UserSourceModel[]>(File.ReadAllText(Path));
        }
    }

    class Accounting : ISource
    {
        public UserSourceModel[] Read()
        {
            throw new NotImplementedException();
        }
    }
}
