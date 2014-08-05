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

        public static IHtmlString RenderCms(this HtmlHelper html, CmsDocument document, CmsView view = null)
        {
            return html.GetCmsContext().Render(view ?? CmsContentView.Default, document.Parts.ToArray()).ToHtml();
        }
    }
}
