using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using MicroCms.SourceCode;

// ReSharper disable once CheckNamespace
namespace MicroCms
{
    public static class SourceCodeConfigurationExtensions
    {
        public static ICmsConfigurator EnableSourceCodeRenderService(this ICmsConfigurator configurator)
        {
            return configurator.RegisterRenderService(SourceCodeCmsRenderService.SourceCodeTypeFamily, new SourceCodeCmsRenderService());
        }
    }
}
