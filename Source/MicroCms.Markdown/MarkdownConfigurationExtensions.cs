using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using MicroCms.Markdown;

// ReSharper disable once CheckNamespace
namespace MicroCms
{
    public static class MarkdownConfigurationExtensions
    {
        public static ICmsConfigurator EnableMarkdownRenderService(this ICmsConfigurator configurator)
        {
            return configurator.RegisterRenderService(MarkdownCmsRenderService.ContentType, new MarkdownCmsRenderService());
        }
    }
}
