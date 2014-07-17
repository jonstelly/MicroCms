using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace MicroCms.Renderers
{
    public class HtmlRenderer : ContentRenderer
    {
        protected override XElement CreateElement(ContentPart part)
        {
            return Parse(part.Value);
        }
    }
}
