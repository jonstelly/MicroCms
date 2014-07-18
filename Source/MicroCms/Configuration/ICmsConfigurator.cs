using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Storage;

namespace MicroCms.Configuration
{
    public interface ICmsConfigurator
    {
        IContentTemplateRepository TemplateRepository { get; set; }
        IContentDocumentRepository DocumentRepository { get; set; }
        ILayoutEngine LayoutEngine { get; set; }
        ICmsConfigurator RegisterBasicRenderers();
        ICmsConfigurator RegisterRenderer(string contentType, IContentRenderer renderer);
    }
}