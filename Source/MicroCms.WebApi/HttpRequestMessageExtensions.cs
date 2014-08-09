using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MicroCms.WebApi
{
    public static class HttpRequestMessageExtensions
    {
        public static readonly string CmsContextKey = "MicroCms-" + Guid.NewGuid();

        public static CmsContext GetCmsContext(this HttpRequestMessage message)
        {
            lock (CmsContextKey)
            {
                if (message.Properties.ContainsKey(CmsContextKey))
                {
                    return (CmsContext)message.Properties[CmsContextKey];
                }

                var context = Cms.CreateContext();
                message.Properties[CmsContextKey] = context;
                message.RegisterForDispose(context);
                return context;
            }
        }
    }
}
