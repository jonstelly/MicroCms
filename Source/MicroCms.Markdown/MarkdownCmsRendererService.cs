using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml.Linq;
using MarkdownSharp;
using MicroCms.Renderers;

namespace MicroCms
{
    public class MarkdownCmsRendererService : CmsRendererService
    {
        public const string ContentType = "markdown";

        protected override XElement CreateElement(CmsPart part)
        {
            return Parse(new Markdown().Transform(part.Value));
        }
    }
}
