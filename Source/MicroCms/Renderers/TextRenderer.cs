using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace MicroCms.Renderers
{
    public class TextRenderer : ContentRenderer
    {
        protected override XElement CreateElement(ContentPart part)
        {
            return Parse(HttpUtility.HtmlEncode(
                part.Value
                    .Replace("\r\n", "<br/>")
                    .Replace("\n", "<br/>")));
        }
    }
}
