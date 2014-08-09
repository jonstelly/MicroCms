using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Configuration;

namespace MicroCms.Tests
{
    public abstract class CmsEnvironmentTests
    {
        protected abstract CmsContext CreateContext(Action<ICmsConfigurator> configure = null);
    }
}
