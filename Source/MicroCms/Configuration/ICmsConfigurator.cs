using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Renderers;
using MicroCms.Search;
using MicroCms.Storage;

namespace MicroCms.Configuration
{
    public interface ICmsConfigurator
    {
        ICmsViewService Views { get; set; }
        ICmsDocumentService Documents { get; set; }
        ICmsSearchService Search { get; set; }
        ICmsConfigurator RegisterBasicRenderServices();
        ICmsConfigurator RegisterRenderService(string contentType, ICmsRenderService renderService);
    }
}