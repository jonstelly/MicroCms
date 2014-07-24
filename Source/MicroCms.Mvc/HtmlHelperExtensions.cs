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
        public static IHtmlString RenderCmsContent(this HtmlHelper html, string contentType, string value)
        {
            return Cms.Render(new CmsPart(contentType, value)).ToHtml();
        }

        public static IHtmlString RenderCms(this HtmlHelper html, CmsPart part)
        {
            return Cms.Render(part).ToHtml();
        }

        public static IHtmlString RenderCms(this HtmlHelper html, CmsView view, params CmsPart[] parts)
        {
            return Cms.Render(view, parts).ToHtml();
        }

        public static IHtmlString RenderCms(this HtmlHelper html, CmsDocument document)
        {
            var area = Cms.GetArea();
            return html.RenderCms(area.DefaultView, document.Parts.ToArray());
        }
    }
}
