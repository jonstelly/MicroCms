using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MicroCms.Renderers
{
    public interface ICmsRendererService
    {
        bool Supports(string contentType);
        XElement Render(CmsPart part);
    }
}
