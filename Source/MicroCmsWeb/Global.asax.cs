using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MicroCms;
using MicroCms.Renderers;
using MicroCms.Storage;

namespace MicroCms
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureCms();
        }

        private void ConfigureCms()
        {
            Cms.Configure(c => c
                    .RegisterBasicRenderers()
                    .RegisterRenderer(ContentTypes.Markdown, new MarkdownRenderer())
                    .RegisterRenderer(ContentTypes.SourceCode, new SourceCodeRenderer()));

            var singleItemTemplate = new ContentTemplate("<div class=\"row\">{0}</div>");
            var template = new ContentTemplate("<div class=\"row\">{0}{1}</div><div class=\"row\">{2}{3}{4}{5}</div><div class=\"row\">{6}</div>");
            Cms.GetArea().TemplateRepository.Save(template);
            Cms.GetArea().TemplateRepository.Save(singleItemTemplate);
            var document = new ContentDocument(template,
                new ContentItem(CreateMarkdown("#4", 4)),
                new ContentItem(CreateMarkdown("#8", 8)),
                new ContentItem(CreateMarkdown("#3", 3)),
                new ContentItem(CreateMarkdown("#3", 3)),
                new ContentItem(CreateMarkdown("#3", 3)),
                new ContentItem(CreateMarkdown("#3", 3)),
                new ContentItem(CreateMarkdown("#12", 12)));
            Cms.GetArea().DocumentRepository.Save(document);
            Cms.GetArea().DocumentRepository.Save(new ContentDocument(singleItemTemplate, new ContentItem(CreateMarkdown("#NEXTDOCUMENT", 12))));
        }

        private ContentPart CreateMarkdown(string value, int columns)
        {
            return new ContentPart(ContentTypes.Markdown, value)
            {
                Attributes = new
                {
                    @class = "col-md-" + columns
                }
            };
        }
    }
}
