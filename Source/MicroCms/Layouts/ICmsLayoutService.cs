using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MicroCms.Layouts
{
    public interface ICmsLayoutService
    {
        XElement Render(CmsTemplate template, params CmsItem[] items);
    }
}
