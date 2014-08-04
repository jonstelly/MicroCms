using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MicroCms
{
    public static class HttpContextExtensions
    {
        public static readonly string CmsContextKey = "MicroCms-" + Guid.NewGuid();

        public static CmsContext GetCmsContext(this HttpContextBase http)
        {
            lock (CmsContextKey)
            {
                var context = (CmsContext) http.Items[CmsContextKey];
                if (context == null)
                {
                    context = Cms.CreateContext();
                    http.Items[CmsContextKey] = context;
                    http.DisposeOnPipelineCompleted(context);
                }
                return context;
            }
        }
    }
}
