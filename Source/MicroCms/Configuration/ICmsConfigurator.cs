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
        IContentTemplateRepository TemplateRepository { get; set; }
        IContentDocumentRepository DocumentRepository { get; set; }
        IContentSearch ContentSearch { get; set; }
        ILayoutEngine LayoutEngine { get; set; }
        ICmsConfigurator RegisterBasicRenderers();
        ICmsConfigurator RegisterRenderer(string contentType, IContentRenderer renderer);
    }
}