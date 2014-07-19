using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using MicroCms.Renderers;

namespace MicroCms.Markdown
{
    public class MarkdownCmsRendererService : CmsRendererService
    {
        public const string ContentType = "markdown";

        protected override XElement CreateElement(CmsPart part)
        {
            return Parse(new MarkdownSharp.Markdown().Transform(part.Value));
        }
    }
}
