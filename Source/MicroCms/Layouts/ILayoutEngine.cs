using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MicroCms.Layouts
{
    public interface ILayoutEngine
    {
        XElement Render(ContentTemplate template, params ContentItem[] items);
    }
}
