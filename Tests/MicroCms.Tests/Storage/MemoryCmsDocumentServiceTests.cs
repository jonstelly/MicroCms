using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroCms.Configuration;
using MicroCms.Storage;
using Xunit;

namespace MicroCms.Tests.Storage
{
    public class MemoryCmsDocumentServiceTests : CmsDocumentServiceTests<MemoryCmsDocumentService>
    {
        protected override void ConfigureDocumentService(ICmsConfigurator configurator)
        {
            configurator.UseMemoryStorage();
        }
    }
}
