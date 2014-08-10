using System;
using System.Collections.Generic;
using System.Text;
using Castle.Windsor;
using MicroCms.Configuration;
using MicroCms.Tests;
using MicroCms.Tests.Configuration;
using Xunit;

namespace MicroCms.Castle.Tests
{
    public class CastleCmsContainerTests : CmsContainerTests
    {
        [Fact]
        public void NullConfigureCmsThrows()
        {
            Assert.Throws<ArgumentNullException>(() => ((IWindsorContainer)null).ConfigureCms());
        }

        protected virtual void SharedConfiguration(ICmsConfigurator configurator)
        {
            configurator
                .UseMemoryStorage()
                .UseHtmlRenderer()
                .UseTextRenderer();
        }

        protected override CmsContext CreateContext(Action<ICmsConfigurator> configure = null)
        {
            var container = new WindsorContainer();
            var configurator = container.ConfigureCms();
            SharedConfiguration(configurator);
            if (configure != null)
                configure(configurator);
            return new TestCmsContext(new CmsContainerProvider(() =>
            {
                var child = new WindsorContainer
                {
                    Parent = container
                };
                return new CastleCmsContainer(child);
            }));
        }
    }
}
