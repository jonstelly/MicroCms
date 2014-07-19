using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MicroCms.Renderers
{
    public interface IContentRenderer
    {
        XElement Render(ContentPart part);
    }
}
