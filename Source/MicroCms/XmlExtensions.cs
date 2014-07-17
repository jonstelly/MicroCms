using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace MicroCms
{
    public static class XmlExtensions
    {
        public static IHtmlString ToHtml(this XElement element)
        {
            using (var sw = new StringWriter())
            {
                element.Save(sw, SaveOptions.DisableFormatting);
                return new HtmlString(sw.ToString());
            }
        }
    }
}
