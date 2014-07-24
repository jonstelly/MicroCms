using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Tests
{
    public static class XmlTestExtensions
    {
        public static void AssertXml(this XElement element, string xml)
        {
            Assert.AreEqual(xml, element.ToString(SaveOptions.DisableFormatting));
        }
    }
}
