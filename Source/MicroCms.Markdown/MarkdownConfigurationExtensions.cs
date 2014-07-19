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
        public static ICmsConfigurator EnableMarkdownRenderer(this ICmsConfigurator configurator)
        {
            return configurator.RegisterRenderer(MarkdownCmsRendererService.ContentType, new MarkdownCmsRendererService());
        }
    }
}
