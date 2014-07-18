using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MicroCms
{
    public static class XmlParser
    {
        public static XElement ParseSafe(string xml)
        {
            try
            {
                var ret = XElement.Parse(xml, LoadOptions.PreserveWhitespace);
                //Force everything to a div
                if (ret.Name.ToString().ToLowerInvariant() != "div")
                    ret = new XElement("div", ret);
                return ret;
            }
            catch (XmlException exception)
            {
                if (exception.Message.StartsWith("There are multiple root elements.")
                    || exception.Message.StartsWith("Data at the root level is invalid."))
                {
                    return ParseSafe("<div>" + xml + "</div>");
                }
                throw new ArgumentOutOfRangeException("Error parsing: " + xml, exception);
            }
        }
    }
}
