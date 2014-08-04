using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MicroCms
{
    public static class CmsContextExtensions
    {
        public static XElement Render(this CmsContext context, CmsPart part)
        {
            var renderService = context.GetRenderService(part.ContentType);
            var element = renderService.Render(context, part);
            return element;
        }

        public static XElement Render(this CmsContext context, CmsView view, params CmsPart[] parts)
        {
            return view.Render(context, new CmsDocument("dynamic", parts)).Single();
        }
    }
}
