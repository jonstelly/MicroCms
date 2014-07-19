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

        public static IHtmlString RenderCms(this HtmlHelper html, CmsItem item)
        {
            return Cms.Render(item).ToHtml();
        }

        public static IHtmlString RenderCms(this HtmlHelper html, CmsTemplate template, params CmsItem[] items)
        {
            return Cms.Render(template, items).ToHtml();
        }

        public static IHtmlString RenderCms(this HtmlHelper html, CmsDocument document)
        {
            var template = Cms.GetArea().Templates.Find(document.TemplateId);
            return html.RenderCms(template, document.Items.ToArray());
        }
    }
}
