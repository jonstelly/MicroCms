using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MicroCms.Renderers
{
    public class HtmlRenderer : IContentRenderer
    {
        public IHtmlString Render(ContentPart part)
        {
            return new HtmlString(part.Value);
        }
    }
}
