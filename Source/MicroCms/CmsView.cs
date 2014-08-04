using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;
using MicroCms.Views;

namespace MicroCms
{
    [KnownType(typeof(CmsContentView))]
    public abstract class CmsView : CmsEntity
    {
        protected CmsView(string title, string documentFormat = "{0}", string partFormat = "{0}")
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

        protected abstract XElement RenderDocument(CmsContext context, CmsDocument document);

        protected abstract XElement RenderPart(CmsContext context, CmsPart part);

        public string DocumentFormat { get; set; }
        public string PartFormat { get; set; }
    }
}
