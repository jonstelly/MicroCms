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
        public CmsContentView(string title, string documentFormat = "{0}", string partFormat = "{0}")
            : base(title, documentFormat, partFormat)
        {
        }

        protected CmsContentView()
        {
        }

        protected override XElement RenderDocument(CmsDocument document)
        {
            return XmlParser.ParseSafe(String.Format(DocumentFormat, String.Join("", document.Parts.Select(RenderPart))));
        }

        protected override XElement RenderPart(CmsPart part)
        {
            return XmlParser.ParseSafe(String.Format(PartFormat, Cms.Render(part)));
        }
    }
}
