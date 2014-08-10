using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Configuration;
using Xunit.Sdk;

namespace MicroCms.Tests
{
    public abstract class CmsEnvironmentTests
    {
        protected abstract CmsContext CreateContext(Action<ICmsConfigurator> configure = null);

        protected Exception AssertThrows(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                return exception;
            }
            throw new AssertException("Expected exception not created");
        }
    }
}
