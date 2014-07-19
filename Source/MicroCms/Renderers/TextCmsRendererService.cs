using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace MicroCms.Renderers
{
    public class TextCmsRendererService : CmsRendererService
    {
        protected override XElement CreateElement(CmsPart part)
        {
            return Parse(HttpUtility.HtmlEncode(
                part.Value
                    .Replace("\r\n", "<br/>")
                    .Replace("\n", "<br/>")));
        }
    }
}
