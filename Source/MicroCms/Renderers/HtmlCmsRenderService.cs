using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using MicroCms.Configuration;

namespace MicroCms.Renderers
{
    [RenderService(CmsTypes.Html)]
    public class HtmlCmsRenderService : CmsRenderService
    {
        public override bool Supports(string contentType)
        {
            if (String.IsNullOrEmpty(contentType))
                return false;
            return contentType.Equals(CmsTypes.Html, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override XElement CreateElement(CmsContext context, CmsPart part)
        {
            return Parse(part.Value);
        }
    }
}
