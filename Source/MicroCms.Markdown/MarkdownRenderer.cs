using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MicroCms.Markdown
{
    public class MarkdownRenderer : IContentRenderer
    {
        private static readonly MarkdownSharp.Markdown _Markdown = new MarkdownSharp.Markdown();

        public IHtmlString Render(ContentPart part)
        {
            return new HtmlString(_Markdown.Transform(part.Value));
        }
    }
}
