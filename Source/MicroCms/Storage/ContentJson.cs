using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MicroCms.Storage
{
    public static class ContentJson
    {
        private static readonly JsonSerializerSettings _Settings = new JsonSerializerSettings()
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
            Formatting = Formatting.Indented,
            TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
            TypeNameHandling = TypeNameHandling.Auto
        };

        public static string Serialize<TContent>(TContent content)
            where TContent : ContentEntity
        {
            var serializer = JsonSerializer.Create(_Settings);
            using (var sw = new StringWriter())
            {
                serializer.Serialize(sw, content, typeof (TContent));
                return sw.ToString();
            }
        }

        public static TContent Deserialize<TContent>(string json)
            where TContent : ContentEntity
        {
            var serializer = JsonSerializer.Create(_Settings);
            using (var sr = new StringReader(json))
            {
                using (var rd = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<TContent>(rd);
                }
            }
        }

        public static TContent Clone<TContent>(TContent content) 
            where TContent : ContentEntity
        {
            var json = Serialize(content);
            return Deserialize<TContent>(json);
        }

    }
}
