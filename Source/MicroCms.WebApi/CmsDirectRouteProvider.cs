using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace MicroCms.WebApi
{
    public class CmsDirectRouteProvider : DefaultDirectRouteProvider
    {
        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            //Honor inherited route attributes, but only for the MicroCms.WebApi assembly.
            // To reduce conflicts and changing global behavior for apps that host MicroCms Web API
            if (IsCmsApiController(actionDescriptor.ControllerDescriptor.ControllerType))
                return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(true);
            return base.GetActionRouteFactories(actionDescriptor);
        }

        private bool IsCmsApiController(Type type)
        {
            var assembly = typeof(CmsDirectRouteProvider).Assembly;
            while (type != null)
            {
                if (type.Assembly == assembly)
                    return true;
                type = type.BaseType;
            }
            return false;
        }
    }
}
