using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace MicroCms
{
    public class CmsView : CmsEntity
    {
        public static readonly CmsView Default = new CmsView("Default");

        public CmsView(string title, string documentFormat = "{0}", string partFormat = "{0}")
        {
            Title = title;
            DocumentFormat = documentFormat;
            PartFormat = partFormat;
        }

        protected CmsView()
        {
        }

        public virtual IEnumerable<XElement> Render(CmsContext context, params CmsDocument[] documents)
        {
            return documents.Select(d => RenderDocument(context, d));
        }

        protected virtual XElement RenderDocument(CmsContext context, CmsDocument document)
        {
            return XmlParser.ParseSafe(String.Format(DocumentFormat, String.Join("", document.Parts.Select(p => RenderPart(context, p)))));
        }

        protected virtual XElement RenderPart(CmsContext context, CmsPart part)
        {
            return XmlParser.ParseSafe(String.Format(PartFormat, context.Render(part)));
        }

        public string DocumentFormat { get; set; }
        public string PartFormat { get; set; }
    }
}
