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
using MicroCms.Azure;
using MicroCms.Azure.Configuration;
using MicroCms.Configuration;
using MicroCms.Lucene;
using MicroCms.Lucene.Configuration;
using MicroCms.Markdown;
using MicroCms.Renderers;
using MicroCms.Search;
using MicroCms.SourceCode;
using MicroCms.Storage;
using MicroCms.Unity;
using MicroCms.Views;
using MicroCms.WebApi;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Practices.Unity;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MicroCms
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly, typeof(CmsDocumentsController).Assembly);
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

            var unity = new UnityContainer();
            var configuration = unity.ConfigureCms()
                .UseHtmlRenderer()
                .UseTextRenderer()
                .UseMarkdownRenderer()
                .UseSourceCodeRenderer();

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (UseAzure)
            {
                //Azure & Lucene
                var cacheDirectory = new RAMDirectory();
                var azureStorageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
                
                //Configure for Azure
                configuration
                    .UseAzureStorage(azureStorageAccount.CreateCloudBlobClient(), "cms")
                    .UseLuceneSearch(new AzureDirectory(azureStorageAccount, "cms-index", cacheDirectory));
            }
            else
            {
                configuration
                    .UseFileSystemStorage(cmsDirectory)
                    .UseLuceneSearch(new SimpleFSDirectory(new DirectoryInfo(Path.Combine(cmsDirectory.FullName, "Index"))));
            }

            Cms.Configure(() => new UnityCmsContainer(unity.CreateChildContainer()));
            using (var context = Cms.CreateContext())
            {
                if (!context.Documents.GetAll().Any())
                {
                    var rowView = new CmsContentView("RowView", "<div class=\"row\">{0}</div>");
                    var sidebarView = new CmsContentView("SidebarView", "<div>{0}</div>");
                    context.Views.Save(rowView);
                    context.Views.Save(sidebarView);
                    var document = new CmsDocument("Example Rows",
                        CreateMarkdown("#MD4", 4),
                        CreateMarkdown("#MD8", 8),
                        CreateMarkdown("#MD3", 3),
                        CreateMarkdown("#MD3", 3),
                        CreateMarkdown("#MD3", 3),
                        CreateMarkdown("#MD3", 3),
                        CreateMarkdown("#MD12", 12));
                    document.Tags.Add("documents");
                    context.Documents.Save(document);
                    context.Documents.Save(new CmsDocument("Source Code Example", CreateMarkdown(@"#CODE
    {{CSharp}}
    public class Thing
    {
        public string Name { get; set; }
    }
", 12)));
                    var sidebar = new CmsDocument("TableOfContents", new CmsPart(CmsTypes.Markdown, @"[Home](/)

[Docs](/docs/)"));
                    sidebar.Tags.Add("TableOfContents");
                    context.Documents.Save(sidebar);

                    var readmeText = File.ReadAllText(Path.Combine(rootFolder, @"..\..\README.md"));

                    var homeDocument = new CmsDocument("Readme", CreateMarkdown(readmeText, 12));
                    homeDocument.Tags.Add("home");
                    Cms.CreateContext().Documents.Save(homeDocument);
                }
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
