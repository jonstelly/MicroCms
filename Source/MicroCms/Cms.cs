using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Renderers;

namespace MicroCms
{
    public static class Cms
    {
        public static ContentTypes GetContentTypes(string name = null)
        {
            name = name ?? String.Empty;
            try
            {
                return _Configurations[name].ContentTypes;
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentOutOfRangeException("name", "No configuration found for: " + name);
            }
        }

        public static void Configure(Action<ICmsConfigurator> action = null)
        {
            Configure(String.Empty, action);
        }

        public static void Configure(string name, Action<ICmsConfigurator> action = null)
        {
            if (_Configurations.ContainsKey(name))
                throw new ArgumentOutOfRangeException("Configuration already specified for: " + name);

            var config = new CmsConfigurator();
            
            if (action == null)
                config.RegisterBasicTypes();
            else
                action(config);

            _Configurations[name] = config.Build();
        }

        private static readonly ConcurrentDictionary<string, CmsConfiguration> _Configurations = new ConcurrentDictionary<string, CmsConfiguration>(); 

        private class CmsConfigurator : ICmsConfigurator
        {
            public CmsConfigurator()
            {
                ContentTypes = new ContentTypes();
            }

            public ICmsConfigurator RegisterBasicTypes()
            {
                Register(ContentTypes.Html, new HtmlRenderer());
                Register(ContentTypes.Text, new TextRenderer());
                return this;
            }

            public ICmsConfigurator Register(string contentType, IContentRenderer renderer)
            {
                ContentTypes.Register(contentType, renderer);
                return this;
            }

            public ContentTypes ContentTypes { get; set; }

            public CmsConfiguration Build()
            {
                return new CmsConfiguration(ContentTypes);
            }
        }
    }
}
