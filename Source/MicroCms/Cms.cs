using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using MicroCms.Configuration;

namespace MicroCms
{
    public static class Cms
    {
        public static XElement Render(CmsView view, params CmsPart[] parts)
        {
            return view.Render(new CmsDocument("dynamic", parts)).Single();
        }
        
        public static XElement Render(CmsPart part)
        {
            var renderService = GetArea().Types.GetRenderService(part.ContentType);
            var element = renderService.Render(part);
            return element;
        }
        
        public static CmsArea GetArea(string area = null)
        {
            area = area ?? String.Empty;
            try
            {
                return _Areas[area];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentOutOfRangeException("area", "No configuration found for: " + area);
            }
        }

        public static CmsArea Configure(Action<ICmsConfigurator> action = null)
        {
            return Configure(String.Empty, action);
        }

        public static CmsArea Configure(string area, Action<ICmsConfigurator> action = null)
        {
            if (_Areas.ContainsKey(area))
                throw new ArgumentOutOfRangeException("Configuration already specified for: " + area);
            
            var config = new CmsConfigurator();
            
            if (action == null)
                config.RegisterBasicRenderServices();
            else
                action(config);

            var ret = config.Build();
            _Areas[area] = ret;
            return ret;
        }

        private static readonly ConcurrentDictionary<string, CmsArea> _Areas = new ConcurrentDictionary<string, CmsArea>(); 

    }
}
