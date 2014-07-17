using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MicroCms;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString RenderContent(this HtmlHelper helper, string contentType, string value)
        {
            return helper.RenderPart(new ContentPart(contentType, value));
        }

        public static IHtmlString RenderItem(this HtmlHelper helper, ContentItem item, string separator = "<br/>")
        {
            return new HtmlString(String.Join(separator, helper.RenderParts(item.Parts)));
        }

        private static IHtmlString RenderPart(this HtmlHelper helper, ContentPart part)
        {
            var renderer = Cms.GetContentTypes().GetRenderer(part.ContentType);
            return renderer.Render(part);
        }

        private static IEnumerable<IHtmlString> RenderParts(this HtmlHelper helper, IEnumerable<ContentPart> parts)
        {
            return parts.Select(helper.RenderPart);
        }
    }
}
