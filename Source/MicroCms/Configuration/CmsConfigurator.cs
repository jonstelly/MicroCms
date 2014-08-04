using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MicroCms.Renderers;
using MicroCms.Search;
using MicroCms.Storage;

namespace MicroCms.Configuration
{
    public abstract class CmsConfigurator : ICmsConfigurator
    {
        protected void RegisterContext()
        {
            RegisterType(typeof (CmsContext), typeof (CmsContext));
        }

        public virtual ICmsConfigurator UseDocService<TDocService>(params object[] parameters)
            where TDocService : ICmsDocumentService
        {
            RegisterType(typeof (ICmsDocumentService), typeof (TDocService), null, parameters);
            return this;
        }

        public virtual ICmsConfigurator UseViewService<TViewService>(params object[] parameters)
            where TViewService : ICmsViewService
        {
            RegisterType(typeof(ICmsViewService), typeof(TViewService), null, parameters);
            return this;
        }

        public virtual ICmsConfigurator UseSearch<TSearchService>(params object[] parameters)
            where TSearchService : ICmsSearchService
        {
            RegisterType(typeof (ICmsSearchService), typeof (TSearchService), null, parameters);
            return this;
        }

        public virtual ICmsConfigurator UseRenderer<TRenderService>()
            where TRenderService : ICmsRenderService
        {
            var renderServiceType = typeof(TRenderService);
            var attributes = renderServiceType.GetCustomAttributes<RenderServiceAttribute>().ToList();
            if (attributes.Count == 0)
                throw new ArgumentOutOfRangeException("RenderServiceAttribute not found on: " + renderServiceType.Name);
            foreach (var attribute in attributes)
            {
                RegisterType(typeof(ICmsRenderService), renderServiceType, attribute.ContentType);
            }
            return this;
        }

        protected abstract void RegisterType(Type from, Type to, string name = null, params object[] parameters);
    }
}