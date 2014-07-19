using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MicroCms.Renderers
{
    public abstract class CmsRendererService : ICmsRendererService
    {
        public XElement Render(CmsPart part)
        {
            var element = CreateElement(part);
            part.ApplyAttributes(element);
            return element;
        }

        protected XElement Parse(string xml)
        {
            return XmlParser.ParseSafe(xml);
        }

        protected abstract XElement CreateElement(CmsPart part);
    }
}
