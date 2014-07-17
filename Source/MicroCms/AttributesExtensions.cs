using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MicroCms
{
    public static class AttributesExtensions
    {
        public static void ApplyAttributes(this IHaveAttributes container, XElement element)
        {
            var attributes = container.Attributes;
            if (attributes != null)
            {
                foreach (var property in attributes.GetType().GetProperties())
                {
                    var value = property.GetValue(attributes);
                    element.Add(new XAttribute(property.Name, value));
                }
            }
        }
    }
}
