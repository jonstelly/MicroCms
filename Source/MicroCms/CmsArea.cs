using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Configuration;
using MicroCms.Layouts;
using MicroCms.Search;
using MicroCms.Storage;

namespace MicroCms
{
    public class CmsArea
    {
        internal CmsArea(CmsConfigurator configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException("configurator");
            
            Types = configurator.Types;
            Layout = configurator.Layout ?? new StringCmsLayoutService();
            
            //TODO: Default to local filesystem / appdata repositories?
            Templates = configurator.Templates ?? new MemoryCmsTemplateService();
            Documents = configurator.Documents ?? new MemoryCmsDocumentService();
            Search = configurator.Search;
        }

        public ICmsLayoutService Layout { get; private set; }
        public CmsTypes Types { get; private set; }
        public ICmsTemplateService Templates { get; private set; }
        public ICmsDocumentService Documents { get; private set; }
        public ICmsSearchService Search { get; private set; }
    }
}
