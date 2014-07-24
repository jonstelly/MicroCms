using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Configuration;
using MicroCms.Search;
using MicroCms.Storage;
using MicroCms.Views;

namespace MicroCms
{
    public class CmsArea
    {
        internal CmsArea(CmsConfigurator configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException("configurator");
            
            Types = configurator.Types;
            
            //TODO: Default to local filesystem / appdata repositories?
            Views = configurator.Views ?? new MemoryCmsViewService();
            Documents = configurator.Documents ?? new MemoryCmsDocumentService();
            Search = configurator.Search;
            DefaultView = new CmsContentView("Default");
            Views.Save(DefaultView);
        }

        public CmsView DefaultView { get; private set; }
        public CmsTypes Types { get; private set; }
        public ICmsViewService Views { get; private set; }
        public ICmsDocumentService Documents { get; private set; }
        public ICmsSearchService Search { get; private set; }
    }
}
