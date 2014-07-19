using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MicroCms.Layouts
{
    public class StringCmsLayoutService : ICmsLayoutService
    {
        public XElement Render(CmsTemplate template, params CmsItem[] items)
        {
            return XmlParser.ParseSafe(String.Format(template.Value, items.Select(Cms.Render).Cast<object>().ToArray()));
        }
    }
}
