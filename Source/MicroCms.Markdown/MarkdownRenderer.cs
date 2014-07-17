using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml.Linq;
using MicroCms.Renderers;

namespace MicroCms
{
    public class MarkdownRenderer : ContentRenderer
    {
        public const string ContentType = "markdown";

        private static readonly MarkdownSharp.Markdown _Markdown = new MarkdownSharp.Markdown();

        protected override XElement CreateElement(ContentPart part)
        {
            return Parse(_Markdown.Transform(part.Value));
        }
    }
}
