using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MicroCms;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class HtmlHelperExtensions
    {
        public static CmsContext GetCmsContext(this HtmlHelper html)
        {
            return html.ViewContext.HttpContext.GetCmsContext();
        }

        public static IHtmlString RenderCms(this HtmlHelper html, CmsDocument document, CmsView view = null)
        {
            return html.GetCmsContext().Render(view ?? CmsView.Default, document.Parts.ToArray()).ToHtml();
        }
    }
}
