using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Lucene.Net.Store;
using MicroCms.Configuration;
using MicroCms.Lucene;
using MicroCms.Lucene.Configuration;
using MicroCms.Markdown;
using MicroCms.SourceCode;
using MicroCms.Storage;
using MicroCms.Tests;
using MicroCms.Unity;
using MicroCms.Views;
using MicroCms.WebApi;
using Microsoft.Owin.Hosting;
using Microsoft.Practices.Unity;
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
            var unity = new UnityContainer();
            unity.ConfigureCms()
                .UseMemoryStorage()
                .UseLuceneSearch(new RAMDirectory())
                .UseTextRenderer()
                .UseHtmlRenderer()
                .UseMarkdownRenderer()
                .UseSourceCodeRenderer();

            CmsTesting.Initialize(() => new UnityCmsContainer(unity.CreateChildContainer()));
            _WebApp = WebApp.Start<Startup>(WebUrl.ToString());
        }

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
            config.MapHttpAttributeRoutes(new CmsDirectRouteProvider());

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
