using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml.Linq;
using MicroCms.Configuration;

namespace MicroCms.Renderers
{
    [RenderService(CmsTypes.Text)]
    public class TextCmsRenderService : CmsRenderService
    {
        public override bool Supports(string contentType)
        {
            if (String.IsNullOrEmpty(contentType))
                return false;
            return contentType.Equals(CmsTypes.Text, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override XElement CreateElement(CmsContext context, CmsPart part)
        {
            return Parse(HttpUtility.HtmlEncode(
                part.Value
                    .Replace("\r\n", "<br/>")
                    .Replace("\n", "<br/>")));
        }
    }
}
