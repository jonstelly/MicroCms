using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MicroCms.Configuration;
using MicroCms.Storage;

namespace MicroCms.Tests.Storage
{
    public class FSCmsViewServiceTests : CmsViewServiceTests<FSCmsViewService>
    {
        private readonly DirectoryInfo _Directory = new DirectoryInfo(@".\" + Guid.NewGuid());

        protected override void ConfigureViewService(ICmsConfigurator configurator)
        {
            configurator.UseFileSystemStorage(_Directory);
        }
    }
}
