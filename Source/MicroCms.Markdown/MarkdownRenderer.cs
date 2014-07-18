using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml.Linq;
using MarkdownSharp;
using MicroCms.Renderers;

namespace MicroCms
{
    public class MarkdownRenderer : ContentRenderer
    {
        public const string ContentType = "markdown";

        protected override XElement CreateElement(ContentPart part)
        {
            return Parse(new Markdown().Transform(part.Value));
        }
    }
}
