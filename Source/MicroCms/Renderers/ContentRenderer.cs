using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MicroCms.Renderers
{
    public abstract class ContentRenderer : IContentRenderer
    {
        public XElement Render(ContentPart part)
        {
            var element = CreateElement(part);
            part.ApplyAttributes(element);
            return element;
        }

        public XElement Parse(string xml)
        {
            try
            {
                return XElement.Parse(xml, LoadOptions.PreserveWhitespace);
            }
            catch (XmlException exception)
            {
                if (exception.Message.StartsWith("There are multiple root elements.")
                    || exception.Message.StartsWith("Data at the root level is invalid."))
                {
                    return Parse("<div>" + xml + "</div>");
                }
                throw new ArgumentOutOfRangeException("Error parsing: " + xml, exception);
            }
        }

        protected abstract XElement CreateElement(ContentPart part);
    }
}
