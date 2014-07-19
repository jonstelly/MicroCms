using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace MicroCms.Renderers
{
    public class HtmlCmsRendererService : CmsRendererService
    {
        public override bool Supports(string contentType)
        {
            if (String.IsNullOrEmpty(contentType))
                return false;
            return contentType.Equals(CmsTypes.Html, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override XElement CreateElement(CmsPart part)
        {
            return Parse(part.Value);
        }
    }
}
