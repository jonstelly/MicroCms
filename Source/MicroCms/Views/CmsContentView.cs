using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MicroCms.Views
{
    public class CmsContentView : CmsView
    {
        public static readonly CmsContentView Default = new CmsContentView("default");

        public CmsContentView(string title, string documentFormat = "{0}", string partFormat = "{0}")
            : base(title, documentFormat, partFormat)
        {
        }

        protected CmsContentView()
        {
        }

        protected override XElement RenderDocument(CmsContext context, CmsDocument document)
        {
            return XmlParser.ParseSafe(String.Format(DocumentFormat, String.Join("", document.Parts.Select(p => RenderPart(context, p)))));
        }

        protected override XElement RenderPart(CmsContext context, CmsPart part)
        {
            return XmlParser.ParseSafe(String.Format(PartFormat, context.Render(part)));
        }
    }
}
