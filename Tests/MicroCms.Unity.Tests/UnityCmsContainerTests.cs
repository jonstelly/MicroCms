using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Configuration;
using MicroCms.Tests;
using MicroCms.Tests.Configuration;
using Microsoft.Practices.Unity;

namespace MicroCms.Unity.Tests
{
    public class UnityCmsContainerTests : CmsContainerTests
    {
        protected virtual void SharedConfiguration(ICmsConfigurator configurator)
        {
            configurator
                .UseMemoryStorage()
                .UseHtmlRenderer()
                .UseTextRenderer();
        }

        protected override CmsContext CreateContext(Action<ICmsConfigurator> configure = null)
        {
            var unity = new UnityContainer();
            var configurator = unity.ConfigureCms();
            SharedConfiguration(configurator);
            if (configure != null)
                configure(configurator);
            return new TestCmsContext(new CmsContainerProvider(() => new UnityCmsContainer(unity)));
        }
    }
}
