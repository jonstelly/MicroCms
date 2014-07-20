using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using MicroCms.Renderers;

namespace MicroCms
{
    public class CmsTypes
    {
        public const string Html = "html";
        public const string Text = "text";
        public const string Markdown = "markdown";
        public const string SourceCode = "code";

        private readonly ConcurrentDictionary<string, ICmsRenderService> _RenderServices = new ConcurrentDictionary<string, ICmsRenderService>();

        internal void Register(string contentType, ICmsRenderService renderService)
        {
            if (contentType == null)
                throw new ArgumentNullException("contentType");
            if (renderService == null)
                throw new ArgumentNullException("renderService");
            
            _RenderServices[contentType.Split('/')[0].ToLowerInvariant()] = renderService;
        }

        public ICmsRenderService GetRenderService(string contentType)
        {
            if (contentType == null)
                throw new ArgumentNullException("contentType");            
            try
            {
                return _RenderServices[contentType.Split('/')[0].ToLowerInvariant()];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentOutOfRangeException("contentType", "No render service registered for: " + contentType);
            }
        }

        public IEnumerable<string> Registered { get { return _RenderServices.Keys; } }
    }
}
