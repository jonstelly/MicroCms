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
        public static XElement Render(ContentTemplate template, params ContentItem[] items)
        {
            return GetArea().LayoutEngine.Render(template, items);
        }

        public static XElement Render(ContentItem item)
        {
            if (item.Parts.Count == 1)
                return Render(item.Parts[0]);

            var element = new XElement("div");
            item.ApplyAttributes(element);
            foreach (var partXml in item.Parts.AsParallel().AsOrdered().Select(Render))
            {
                element.Add(partXml);
            }
            return element;
        }
        
        public static XElement Render(ContentPart part)
        {
            var renderer = GetArea().ContentTypes.GetRenderer(part.ContentType);
            var element = renderer.Render(part);
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

        public static void Configure(Action<ICmsConfigurator> action = null)
        {
            Configure(String.Empty, action);
        }

        public static void Configure(string area, Action<ICmsConfigurator> action = null)
        {
            if (_Areas.ContainsKey(area))
                throw new ArgumentOutOfRangeException("Configuration already specified for: " + area);
            
            var config = new CmsConfigurator();
            
            if (action == null)
                config.RegisterBasicRenderers();
            else
                action(config);

            _Areas[area] = config.Build();
        }

        private static readonly ConcurrentDictionary<string, CmsArea> _Areas = new ConcurrentDictionary<string, CmsArea>(); 

    }
}
