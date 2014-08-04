using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MicroCms.Renderers;
using MicroCms.Storage;

namespace MicroCms.Configuration
{
    public static class CmsConfiguratorExtensions
    {
        public static ICmsConfigurator UseMemoryStorage(this ICmsConfigurator configurator)
        {
            configurator.UseDocService<MemoryCmsDocumentService>(new ConcurrentDictionary<Guid, CmsDocument>());
            configurator.UseViewService<MemoryCmsViewService>(new ConcurrentDictionary<Guid, CmsView>());
            return configurator;
        }

        public static ICmsConfigurator UseFileSystemStorage(this ICmsConfigurator configurator, DirectoryInfo directory)
        {
            configurator.UseDocService<FSCmsDocumentService>(directory);
            configurator.UseViewService<FSCmsViewService>(directory);
            return configurator;
        }

        public static ICmsConfigurator UseHtmlRenderer(this ICmsConfigurator configurator)
        {
            configurator.UseRenderer<HtmlCmsRenderService>();
            return configurator;
        }

        public static ICmsConfigurator UseTextRenderer(this ICmsConfigurator configurator)
        {
            configurator.UseRenderer<TextCmsRenderService>();
            return configurator;
        }
    }
}
