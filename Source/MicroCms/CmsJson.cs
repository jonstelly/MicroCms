using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Text;
using Newtonsoft.Json;

namespace MicroCms
{
    public class CmsJson
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
            Formatting = Formatting.Indented,
            TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
            TypeNameHandling = TypeNameHandling.Auto
        };

        public static string Serialize<TContent>(TContent content)
            where TContent : CmsEntity
        {
            var serializer = JsonSerializer.Create(Settings);
            using (var sw = new StringWriter())
            {
                serializer.Serialize(sw, content, typeof(TContent));
                return sw.ToString();
            }
        }

        public static TContent Deserialize<TContent>(string json)
            where TContent : CmsEntity
        {
            var serializer = JsonSerializer.Create(Settings);
            using (var sr = new StringReader(json))
            {
                using (var rd = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<TContent>(rd);
                }
            }
        }

        public static TContent Clone<TContent>(TContent content)
            where TContent : CmsEntity
        {
            var json = Serialize(content);
            return Deserialize<TContent>(json);
        }
    }
}
