using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using MicroCms.Configuration;

namespace MicroCms
{
    public static class Cms
    {
        public static CmsContext CreateContext()
        {
            return new CmsContext(_ContainerProvider);
        }

        public static void Configure(Func<ICmsContainer> containerFunc, bool disposeOnComplete = true)
        {
            lock (typeof (Cms))
            {
                if (_ContainerProvider != null)
                    throw new InvalidOperationException("Multiple calls to Cms.Configure()");

                if (containerFunc == null)
                    throw new ArgumentNullException("containerFunc");

                _ContainerProvider = new CmsContainerProvider(containerFunc, disposeOnComplete);
            }
        }

        private static ICmsContainerProvider _ContainerProvider;
    }
}
