using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using MicroCms.Renderers;

namespace MicroCms
{
    public class ContentTypes
    {
        public const string Html = "html";
        public const string Text = "text";
    
        private readonly ConcurrentDictionary<string, IContentRenderer> _Renderers = new ConcurrentDictionary<string, IContentRenderer>();

        internal void Register(string contentType, IContentRenderer renderer)
        {
            if (contentType == null)
                throw new ArgumentNullException("contentType");
            if (renderer == null)
                throw new ArgumentNullException("renderer");
            
            _Renderers[contentType.Split('/')[0].ToLowerInvariant()] = renderer;
        }

        public IContentRenderer GetRenderer(string contentType)
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
