using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Xml.Linq;
using MicroCms;
using MicroCms.Views;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class HtmlHelperExtensions
    {
        public static CmsContext GetCmsContext(this HtmlHelper html)
        {
            return html.ViewContext.HttpContext.GetCmsContext();
        }

        public static IHtmlString RenderCmsContent(this HtmlHelper html, string contentType, string value)
        {
            return html.GetCmsContext().Render(new CmsPart(contentType, value)).ToHtml();
        }

        public static IHtmlString RenderCms(this HtmlHelper html, CmsPart part)
        {
            return html.GetCmsContext().Render(part).ToHtml();
        }

        public static IHtmlString RenderCms(this HtmlHelper html, CmsView view, params CmsPart[] parts)
        {
            return html.GetCmsContext().Render(view, parts).ToHtml();
        }

        public static IHtmlString RenderCms(this HtmlHelper html, CmsDocument document)
        {
            return html.GetCmsContext().Render(CmsContentView.Default, document.Parts.ToArray()).ToHtml();
        }
    }
}
