using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using MicroCms.Storage;

// ReSharper disable once CheckNamespace
namespace MicroCms
{
    public static class CmsConfigurationExtensions
    {
        public static ICmsConfigurator UseFileSystemStorage(this ICmsConfigurator configurator, DirectoryInfo directory)
        {
            configurator.Templates = new FSCmsTemplateService(directory);
            configurator.Documents = new FSCmsDocumentService(directory);
            return configurator;
        }

        public static ICmsConfigurator UseMemoryStorage(this ICmsConfigurator configurator)
        {
            configurator.Templates = new MemoryCmsTemplateService();
            configurator.Documents = new MemoryCmsDocumentService();
            return configurator;
        }
    }
}
