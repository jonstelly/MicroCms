using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Layouts;
using MicroCms.Renderers;
using MicroCms.Search;
using MicroCms.Storage;

namespace MicroCms.Configuration
{
    public interface ICmsConfigurator
    {
        ICmsTemplateService Templates { get; set; }
        ICmsDocumentService Documents { get; set; }
        ICmsSearchService Search { get; set; }
        ICmsLayoutService Layout { get; set; }
        ICmsConfigurator RegisterBasicRenderers();
        ICmsConfigurator RegisterRenderer(string contentType, ICmsRendererService renderer);
    }
}