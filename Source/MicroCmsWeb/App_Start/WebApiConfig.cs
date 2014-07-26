using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MicroCms.WebApi;

namespace MicroCms
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.JsonFormatter.SerializerSettings = CmsJson.Settings;

            // Web API routes
            config.MapHttpAttributeRoutes(new CmsDirectRouteProvider());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

        }
    }
}
