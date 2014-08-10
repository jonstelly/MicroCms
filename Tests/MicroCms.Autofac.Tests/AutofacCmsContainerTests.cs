using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MicroCms.Configuration;
using MicroCms.Tests;
using MicroCms.Tests.Configuration;
using Xunit;

namespace MicroCms.Autofac.Tests
{
    public class AutofacCmsContainerTests : CmsContainerTests
    {
        [Fact]
        public void NullConfigureCmsThrows()
        {
            Assert.Throws<ArgumentNullException>(() => ((ContainerBuilder)null).ConfigureCms());
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
            var builder = new ContainerBuilder();
            var configurator = builder.ConfigureCms();
            SharedConfiguration(configurator);
            if (configure != null)
                configure(configurator);
            return new TestCmsContext(new CmsContainerProvider(() => new AutofacCmsContainer(builder.Build())));
        }
    }
}
