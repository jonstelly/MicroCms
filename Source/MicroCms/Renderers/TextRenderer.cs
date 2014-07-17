using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MicroCms.Renderers
{
    public class TextRenderer : IContentRenderer
    {
        public IHtmlString Render(ContentPart part)
        {
            return new HtmlString(HttpUtility.HtmlEncode(
                part.Value
                .Replace("\r\n", "<br/>")
                .Replace("\n", "<br/>")));
        }
    }
}
