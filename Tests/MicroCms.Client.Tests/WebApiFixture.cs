using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Lucene.Net.Store;
using MicroCms.Configuration;
using MicroCms.Lucene.Configuration;
using MicroCms.Unity;
using MicroCms.WebApi;
using Microsoft.Owin.Hosting;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Owin;

namespace MicroCms.Client.Tests
{
    public class WebApiFixture : IDisposable
    {
        private readonly IDisposable _WebApp;
        public static readonly Uri WebUrl = new Uri("http://localhost:9000/");

        public CmsDocument ExampleDocument { get; private set; }
        public CmsView ExampleView { get; private set; }

        public WebApiFixture()
        {
            var unity = new UnityContainer();
            unity.ConfigureCms()
                .UseMemoryStorage()
                .UseLuceneSearch(new RAMDirectory())
                .UseTextRenderer()
                .UseHtmlRenderer()
                .UseMarkdownRenderer()
                .UseSourceCodeRenderer();
            Cms.Configure(() => new UnityCmsContainer(unity.CreateChildContainer()));
            using (var context = Cms.CreateContext())
            {
                ExampleDocument = new CmsDocument("Example");
                context.Documents.Save(ExampleDocument);
                ExampleView = new CmsView("Example");
                context.Views.Save(ExampleView);
            }

            //CmsTesting.Initialize(() => new UnityCmsContainer(unity.CreateChildContainer()));
            _WebApp = WebApp.Start<Startup>(WebUrl.ToString());
        }

        private class Startup
        {
            // This code configures Web API. The Startup class is specified as a type
            // parameter in the WebApp.Start method.
            public void Configuration(IAppBuilder appBuilder)
            {
                // Configure Web API for self-host. 
                var config = new HttpConfiguration();
                //config.Services.Replace(typeof(IAssembliesResolver), new MicroCmsApiResolver());
                config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;

                // Web API configuration and services
                config.Formatters.JsonFormatter.SerializerSettings = CmsJson.Settings;

                // Web API routes
                config.MapHttpAttributeRoutes(new CmsDirectRouteProvider());

                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/cms/{controller}/{id}",
                    defaults: new
                    {
                        id = RouteParameter.Optional
                    });

                appBuilder.UseWebApi(config);
            }
        }

        public void Dispose()
        {
            _WebApp.Dispose();
        }
    } 
}
