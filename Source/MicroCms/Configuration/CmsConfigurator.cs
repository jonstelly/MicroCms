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
            ContentTypes = new ContentTypes();
        }

        public ILayoutEngine LayoutEngine { get; set; }
        public IContentTemplateRepository TemplateRepository { get; set; }
        public IContentDocumentRepository DocumentRepository { get; set; }
        public IContentSearch ContentSearch { get; set; }
        public ContentTypes ContentTypes { get; private set; }

        public ICmsConfigurator RegisterBasicRenderers()
        {
            RegisterRenderer(ContentTypes.Html, new HtmlRenderer());
            RegisterRenderer(ContentTypes.Text, new TextRenderer());
            return this;
        }

        public ICmsConfigurator RegisterRenderer(string contentType, IContentRenderer renderer)
        {
            ContentTypes.Register(contentType, renderer);
            return this;
        }

        public CmsArea Build()
        {
            return new CmsArea(this);
        }
    }
}