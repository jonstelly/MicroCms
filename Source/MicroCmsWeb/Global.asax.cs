using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Lucene.Net.Store;
using MicroCms.Lucene;
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
            var rootFolder = Server.MapPath("~/");
            var cmsDirectory = new DirectoryInfo(Path.Combine(rootFolder, @"App_Data\Cms"));

            Cms.Configure(c =>
            {
                c
                    .RegisterBasicRenderers()
                    .RegisterRenderer(ContentTypes.Markdown, new MarkdownRenderer())
                    .RegisterRenderer(ContentTypes.SourceCode, new SourceCodeRenderer());
                c.DocumentRepository = new FileSystemContentDocumentRepository(cmsDirectory);
                c.TemplateRepository = new FileSystemContentTemplateRepository(cmsDirectory);
                c.ContentSearch = new LuceneContentSearch(new SimpleFSDirectory(new DirectoryInfo(Path.Combine(cmsDirectory.FullName, "Index"))));
            });

            var singleItemTemplate = new ContentTemplate("<div class=\"row\">{0}</div>");
            var template = new ContentTemplate("<div class=\"row\">{0}{1}</div><div class=\"row\">{2}{3}{4}{5}</div><div class=\"row\">{6}</div>");
            Cms.GetArea().TemplateRepository.Save(template);
            Cms.GetArea().TemplateRepository.Save(singleItemTemplate);
            var document = new ContentDocument(template, "Example Rows",
                new ContentItem(CreateMarkdown("#4", 4)),
                new ContentItem(CreateMarkdown("#8", 8)),
                new ContentItem(CreateMarkdown("#3", 3)),
                new ContentItem(CreateMarkdown("#3", 3)),
                new ContentItem(CreateMarkdown("#3", 3)),
                new ContentItem(CreateMarkdown("#3", 3)),
                new ContentItem(CreateMarkdown("#12", 12))) { Path = "documents" };
            Cms.GetArea().DocumentRepository.Save(document);
            Cms.GetArea().DocumentRepository.Save(new ContentDocument(singleItemTemplate, "Following Example Row", new ContentItem(CreateMarkdown("#NEXTDOCUMENT", 12))));

            var readmeText = File.ReadAllText(Path.Combine(rootFolder, @"..\..\README.md"));

            var homeDocument = new ContentDocument(singleItemTemplate, "Readme", new ContentItem(CreateMarkdown(readmeText, 12)))
            {
                Path = "home"
            };
            Cms.GetArea().DocumentRepository.Save(homeDocument);
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
