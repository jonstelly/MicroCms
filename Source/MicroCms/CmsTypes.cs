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

        private readonly ConcurrentDictionary<string, ICmsRendererService> _Renderers = new ConcurrentDictionary<string, ICmsRendererService>();

        internal void Register(string contentType, ICmsRendererService renderer)
        {
            if (contentType == null)
                throw new ArgumentNullException("contentType");
            if (renderer == null)
                throw new ArgumentNullException("renderer");
            
            _Renderers[contentType.Split('/')[0].ToLowerInvariant()] = renderer;
        }

        public ICmsRendererService GetRenderer(string contentType)
        {
            if (contentType == null)
                throw new ArgumentNullException("contentType");            
            try
            {
                return _Renderers[contentType.Split('/')[0].ToLowerInvariant()];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentOutOfRangeException("contentType", "No renderer registered for: " + contentType);
            }
        }

        public IEnumerable<string> Registered { get { return _Renderers.Keys; } }
    }
}
