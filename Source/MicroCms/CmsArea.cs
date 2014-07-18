using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Configuration;
using MicroCms.Layouts;
using MicroCms.Storage;

namespace MicroCms
{
    public class CmsArea
    {
        internal CmsArea(CmsConfigurator configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException("configurator");
            
            ContentTypes = configurator.ContentTypes;
            LayoutEngine = configurator.LayoutEngine ?? new StringFormatLayoutEngine();
            
            //TODO: Default to local filesystem / appdata repositories?
            TemplateRepository = configurator.TemplateRepository ?? new MemoryContentTemplateRepository();
            DocumentRepository = configurator.DocumentRepository ?? new MemoryContentDocumentRepository();
        }

        public ILayoutEngine LayoutEngine { get; private set; }
        public ContentTypes ContentTypes { get; private set; }
        public IContentTemplateRepository TemplateRepository { get; private set; }
        public IContentDocumentRepository DocumentRepository { get; private set; }
    }
}
