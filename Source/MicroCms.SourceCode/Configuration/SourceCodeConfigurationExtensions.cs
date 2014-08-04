using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using MicroCms.SourceCode;

// ReSharper disable once CheckNamespace
namespace MicroCms.Configuration
{
    public static class SourceCodeConfigurationExtensions
    {
        public static ICmsConfigurator UseSourceCodeRenderer(this ICmsConfigurator configurator)
        {
            configurator.UseRenderer<SourceCodeCmsRenderService>();
            return configurator;
        }
    }
}
