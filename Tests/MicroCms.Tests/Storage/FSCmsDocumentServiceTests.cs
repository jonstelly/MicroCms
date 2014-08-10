using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using MicroCms.Storage;
using Xunit;

namespace MicroCms.Tests.Storage
{
    public class FSCmsDocumentServiceTests : CmsDocumentServiceTests<FSCmsDocumentService>
    {
        private readonly DirectoryInfo _Directory = new DirectoryInfo(@".\" + Guid.NewGuid());

        protected override void ConfigureDocumentService(ICmsConfigurator configurator)
        {
            configurator.UseFileSystemStorage(_Directory);
        }
    }
}
