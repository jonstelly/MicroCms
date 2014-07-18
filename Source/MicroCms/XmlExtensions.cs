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
        private static readonly XmlWriterSettings _Settings = new XmlWriterSettings
        {
            ConformanceLevel = ConformanceLevel.Fragment,
            OmitXmlDeclaration = true,
            Encoding = Encoding.UTF8
        };

        public static IHtmlString ToHtml(this XElement element)
        {
            var sb = new StringBuilder();
            using (var wr = XmlWriter.Create(sb, _Settings))
            {
                element.WriteTo(wr);
            }
            return new HtmlString(sb.ToString());

            //using (var sw = new StringWriter())
            //{
            //    element.Save(sw, SaveOptions.DisableFormatting);
            //    return new HtmlString(sw.ToString());
            //}
        }
    }
}
