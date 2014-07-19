using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MicroCms.Renderers
{
    public interface ICmsRendererService
    {
        XElement Render(CmsPart part);
    }
}
