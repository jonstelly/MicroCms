using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Layouts;
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

        public ICmsLayoutService Layout { get; set; }
        public ICmsTemplateService Templates { get; set; }
        public ICmsDocumentService Documents { get; set; }
        public ICmsSearchService Search { get; set; }
        public CmsTypes Types { get; private set; }

        public ICmsConfigurator RegisterBasicRenderers()
        {
            RegisterRenderer(CmsTypes.Html, new HtmlCmsRendererService());
            RegisterRenderer(CmsTypes.Text, new TextCmsRendererService());
            return this;
        }

        public ICmsConfigurator RegisterRenderer(string contentType, ICmsRendererService renderer)
        {
            Types.Register(contentType, renderer);
            return this;
        }

        public CmsArea Build()
        {
            return new CmsArea(this);
        }
    }
}