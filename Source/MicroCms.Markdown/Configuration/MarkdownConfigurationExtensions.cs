using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Markdown;

// ReSharper disable once CheckNamespace
namespace MicroCms.Configuration
{
    public static class MarkdownConfigurationExtensions
    {
        public static ICmsConfigurator UseMarkdownRenderer(this ICmsConfigurator configurator)
        {
            configurator.UseRenderer<MarkdownCmsRenderService>();
            return configurator;
        }
    }
}
