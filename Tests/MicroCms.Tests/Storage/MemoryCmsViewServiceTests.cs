using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Configuration;
using MicroCms.Storage;
using Xunit;

namespace MicroCms.Tests.Storage
{
    public class MemoryCmsViewServiceTests : CmsViewServiceTests<MemoryCmsViewService>
    {
        [Fact]
        public void NoArgsConstructorSucceeds()
        {
            Assert.NotNull(new MemoryCmsViewService());
        }

        protected override void ConfigureViewService(ICmsConfigurator configurator)
        {
            configurator.UseMemoryStorage();
        }
    }
}
