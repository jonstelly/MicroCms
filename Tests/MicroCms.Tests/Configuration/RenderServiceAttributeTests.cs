using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MicroCms.Configuration;
using MicroCms.Renderers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Tests.Configuration
{
    [TestClass]
    public class RenderServiceAttributeTests
    {
        [RenderService("first")]
        [RenderService("second")]
        public class TestMultipleRenderService : CmsRenderService
        {
            public override bool Supports(string contentType)
            {
                return contentType.Equals("first", StringComparison.InvariantCultureIgnoreCase) || contentType.Equals("second", StringComparison.InvariantCultureIgnoreCase);
            }

            protected override XElement CreateElement(CmsContext context, CmsPart part)
            {
                return null;
            }
        }

        [TestMethod]
        public void UnitTest()
        {
            var renderServices = RenderServiceAttribute.GetMappings(typeof (RenderServiceAttributeTests).Assembly);
            Assert.IsNotNull(renderServices);
            Assert.AreEqual(2, renderServices.Count);
            Assert.AreEqual(typeof (TestMultipleRenderService), renderServices["first"]);
            Assert.AreEqual(typeof (TestMultipleRenderService), renderServices["second"]);
        }
         
    }
}
