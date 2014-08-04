using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MicroCms.Renderers;

namespace MicroCms.Configuration
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RenderServiceAttribute : Attribute
    {
        public static Dictionary<string, Type> GetMappings(params Assembly[] rendererAssemblies)
        {
            var mappings = new Dictionary<string, Type>();

            //Iterate through assemblies.  Assembly order matters, later assembly renderers can overwrite previously registered renderers
            foreach (var rendererAssembly in rendererAssemblies)
            {
                foreach (var renderer in rendererAssembly.GetTypes().Where(t => typeof(ICmsRenderService).IsAssignableFrom(t)
                                                                                && t.IsClass
                                                                                && !t.IsAbstract
                                                                                && t.GetCustomAttributes<RenderServiceAttribute>() != null))
                {
                    foreach (var rsa in renderer.GetCustomAttributes<RenderServiceAttribute>())
                    {
                        mappings[rsa.ContentType.ToLowerInvariant()] = renderer;
                    }
                }
            }
            return mappings;
        }

        public RenderServiceAttribute(string contentType)
        {
            if (String.IsNullOrEmpty(contentType))
                throw new ArgumentNullException("contentType");
            
            ContentType = contentType;
        }

        public string ContentType { get; set; }
    }
}
