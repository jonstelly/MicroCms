using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using MicroCms.Configuration;
using MicroCms.Renderers;
using MicroCms.Search;
using MicroCms.Storage;

namespace MicroCms
{
    public class CmsContext : IDisposable
    {
        internal CmsContext(ICmsContainerProvider containerProvider)
        {
            if (containerProvider == null)
                throw new ArgumentNullException("containerProvider");
            
            Container = containerProvider.GetContainer();
            _OwnsContainer = containerProvider.DisposeOnComplete;
            _Search = new Lazy<ICmsSearchService>(() =>
            {
                try
                {
                    return Container.Resolve<ICmsSearchService>();
                }
                catch(Exception)
                {
                    return null;
                }
            });
        }

        public static string GetContentFamily(string contentType)
        {
            return contentType.Split('/').First();
        }

        public ICmsRenderService GetRenderService(string contentType)
        {
            contentType = contentType.ToLowerInvariant();
            try
            {
                return Container.Resolve<ICmsRenderService>(contentType);
            }
            catch (Exception)
            {
                return Container.Resolve<ICmsRenderService>(GetContentFamily(contentType));
            }
        }

        public ICmsContainer Container { get; private set; }
        public ICmsViewService Views { get { return Container.Resolve<ICmsViewService>(); } }
        public ICmsDocumentService Documents { get { return Container.Resolve<ICmsDocumentService>(); } }
        private readonly Lazy<ICmsSearchService> _Search;
        public ICmsSearchService Search { get { return _Search.Value; } }

        private readonly bool _OwnsContainer;

        public void Dispose()
        {
            if (_OwnsContainer)
                Container.Dispose();
        }
    }
}
