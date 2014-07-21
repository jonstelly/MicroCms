using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Lucene.Net.Store;
using MicroCms.WebApi;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;

namespace MicroCms.Client.Tests
{
    [TestClass]
    public class Startup
    {
        private static IDisposable _WebApp;
        public static readonly Uri WebUrl = new Uri("http://localhost:9000/");

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            var area = Cms.Configure(c =>
            {
                c.RegisterBasicRenderServices()
                    .EnableMarkdownRenderService()
                    .EnableSourceCodeRenderService()
                    .UseLuceneSearch(new RAMDirectory());
                c.UseMemoryStorage();
            });
            ExampleTemplate = new CmsTemplate("ExampleTemplate", "<div>{0}</div>");
            area.Templates.Save(ExampleTemplate);
            ExampleDocument = new CmsDocument(ExampleTemplate, "ExampleDocument", new CmsItem(new CmsPart(CmsTypes.Markdown, "#Hello, World")));
            area.Documents.Save(ExampleDocument);

            _WebApp = WebApp.Start<Startup>(WebUrl.ToString());
        }

        public static CmsDocument ExampleDocument { get; private set; }
        public static CmsTemplate ExampleTemplate { get; private set; }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            _WebApp.Dispose();
        }

        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            //config.Services.Replace(typeof(IAssembliesResolver), new MicroCmsApiResolver());

            // Web API configuration and services
            config.Formatters.JsonFormatter.SerializerSettings = CmsJson.Settings;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/cms/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            appBuilder.UseWebApi(config);
        }

        private class MicroCmsApiResolver : DefaultAssembliesResolver
        {
            public override ICollection<Assembly> GetAssemblies()
            {
                var assemblies = base.GetAssemblies();
                assemblies.Add(typeof (CmsDocumentsController).Assembly);
                return assemblies;
            }
        }
    } 
}
