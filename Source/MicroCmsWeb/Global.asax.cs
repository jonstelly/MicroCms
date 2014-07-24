using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Lucene.Net.Store;
using Lucene.Net.Store.Azure;
using MicroCms.Views;
using MicroCms.WebApi;
using Microsoft.WindowsAzure.Storage;

namespace MicroCms
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly, typeof (CmsDocumentsController).Assembly);
            _Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_Container));
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureCms();
        }

        private static IContainer _Container;
        private const bool UseAzure = false;

        private void ConfigureCms()
        {
            var rootFolder = Server.MapPath("~/");
            var cmsDirectory = new DirectoryInfo(Path.Combine(rootFolder, @"App_Data\Cms"));

            if (cmsDirectory.Exists)
            {
                cmsDirectory.Delete(true);
            }

            CmsArea cms = null;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (UseAzure)
            {
                //Configure for Azure
                cms = Cms.Configure(c =>
                {
                    var cacheDirectory = new RAMDirectory();
                    var azureDirectory = new AzureDirectory(CloudStorageAccount.Parse("UseDevelopmentStorage=true"), "cms-index", cacheDirectory);
                    c.RegisterBasicRenderServices()
                        .EnableMarkdownRenderService()
                        .EnableSourceCodeRenderService()
                        .UseLuceneSearch(azureDirectory);
                    c.UseAzureStorage("UseDevelopmentStorage=true");
                });
            }
            else
            {
                //Configure for local filesystem
                cms = Cms.Configure(c =>
                {
                    c.RegisterBasicRenderServices()
                        .EnableMarkdownRenderService()
                        .EnableSourceCodeRenderService()
                        .UseLuceneSearch(new SimpleFSDirectory(new DirectoryInfo(Path.Combine(cmsDirectory.FullName, "Index"))));
                    c.UseFileSystemStorage(cmsDirectory);
                });
            }

            if (!cms.Documents.GetAll().Any())
            {
                var rowView = new CmsContentView("RowView", "<div class=\"row\">{0}</div>");
                var sidebarView = new CmsContentView("SidebarView", "<div>{0}</div>");
                cms.Views.Save(rowView);
                cms.Views.Save(sidebarView);
                var document = new CmsDocument("Example Rows", 
                    CreateMarkdown("#MD4", 4),
                    CreateMarkdown("#MD8", 8),
                    CreateMarkdown("#MD3", 3),
                    CreateMarkdown("#MD3", 3),
                    CreateMarkdown("#MD3", 3),
                    CreateMarkdown("#MD3", 3),
                    CreateMarkdown("#MD12", 12));
                document.Tags.Add("documents");
                cms.Documents.Save(document);
                cms.Documents.Save(new CmsDocument("Source Code Example", CreateMarkdown(@"#CODE
    {{CSharp}}
    public class Thing
    {
        public string Name { get; set; }
    }
", 12)));
                var sidebar = new CmsDocument("TableOfContents", new CmsPart(CmsTypes.Markdown, @"[Home](/)

[Docs](/docs/)"));
                sidebar.Tags.Add("TableOfContents");
                cms.Documents.Save(sidebar);

                var readmeText = File.ReadAllText(Path.Combine(rootFolder, @"..\..\README.md"));

                var homeDocument = new CmsDocument("Readme", CreateMarkdown(readmeText, 12));
                homeDocument.Tags.Add("home");
                Cms.GetArea().Documents.Save(homeDocument);
            }
        }

        private CmsPart CreateMarkdown(string value, int columns)
        {
            return new CmsPart(CmsTypes.Markdown, value, new
            {
                @class = "col-md-" + columns
            });
        }
    }
}
