using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MicroCms.Configuration;
using MicroCms.Renderers;
using Xunit;

namespace MicroCms.Tests.Configuration
{
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

        [Fact]
        public void GetMultipleMappingsSucceeds()
        {
            var renderServices = RenderServiceAttribute.GetMappings(typeof (RenderServiceAttributeTests).Assembly);
            Assert.NotNull(renderServices);
            Assert.Equal(2, renderServices.Count);
            Assert.Equal(typeof (TestMultipleRenderService), renderServices["first"]);
            Assert.Equal(typeof (TestMultipleRenderService), renderServices["second"]);
        }
         
    }
}
