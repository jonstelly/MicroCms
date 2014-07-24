using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Renderers;
using MicroCms.Search;
using MicroCms.Storage;

namespace MicroCms.Configuration
{
    internal class CmsConfigurator : ICmsConfigurator
    {
        public CmsConfigurator()
        {
            Types = new CmsTypes();
        }

        public ICmsViewService Views { get; set; }
        public ICmsDocumentService Documents { get; set; }
        public ICmsSearchService Search { get; set; }
        public CmsTypes Types { get; private set; }

        public ICmsConfigurator RegisterBasicRenderServices()
        {
            RegisterRenderService(CmsTypes.Html, new HtmlCmsRenderService());
            RegisterRenderService(CmsTypes.Text, new TextCmsRenderService());
            return this;
        }

        public ICmsConfigurator RegisterRenderService(string contentType, ICmsRenderService renderService)
        {
            Types.Register(contentType, renderService);
            return this;
        }

        public CmsArea Build()
        {
            return new CmsArea(this);
        }
    }
}