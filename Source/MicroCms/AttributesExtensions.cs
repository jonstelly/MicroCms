using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MicroCms
{
    public static class AttributesExtensions
    {
        public static void ApplyAttributes(this Dictionary<string, string> attributes, XElement element)
        {
            if (attributes == null)
                return;

            foreach (var attribute in attributes)
            {
                element.Add(new XAttribute(attribute.Key, attribute.Value));
            }
        }

        public static Dictionary<string, string> ToAttributeDictionary(this object attributes)
        {
            var values = new Dictionary<string, string>();
            if (attributes != null)
            {
                foreach (var property in attributes.GetType().GetProperties())
                {
                    var value = property.GetValue(attributes);
                    values[property.Name] = value != null ? value.ToString() : String.Empty;
                }
            }

            return values;
        }
    }
}
