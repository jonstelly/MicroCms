using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Xml.Linq;
using MicroCms;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString RenderContent(this HtmlHelper html, string contentType, string value)
        {
            return html.RenderPart(new ContentPart(contentType, value)).ToHtml();
        }

        public static IHtmlString RenderItem(this HtmlHelper html, ContentItem item, string separator = "<br/>")
        {
            var div = new XElement("div");
            item.ApplyAttributes(div);
            div.Add(html.RenderParts(item.Parts));
            return div.ToHtml();
        }

        private static XElement RenderPart(this HtmlHelper html, ContentPart part)
        {
            var renderer = Cms.GetContentTypes().GetRenderer(part.ContentType);
            var element = renderer.Render(part);
            part.ApplyAttributes(element);
            return element;
        }

        private static IEnumerable<XElement> RenderParts(this HtmlHelper html, IEnumerable<ContentPart> parts)
        {
            return parts.Select(html.RenderPart);
        }
    }
}
