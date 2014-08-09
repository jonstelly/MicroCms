using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Configuration;
using MicroCms.Views;
using Xunit;

namespace MicroCms.Tests
{
    public class CmsTests : CmsUnityTests
    {
        [Fact]
        public void CreateContextSucceeds()
        {
            using (var context = CreateContext())
            {
                Assert.NotNull(context);
            }
        }
        
        [Fact]
        public void RenderSimpleTextPartSucceeds()
        {
            using (var context = CreateContext())
            {
                var xml = context.Render(new CmsPart(CmsTypes.Text, "Hello"));
                Assert.NotNull(xml);
                Assert.Equal("<div>Hello</div>", xml.ToString());
            }
        }

        [Fact]
        public void RenderViewAndPartsSucceeds()
        {
            using (var context = CreateContext())
            {
                var xml = context.Render(new CmsContentView("test"), new CmsPart(CmsTypes.Text, "Hello"));
                Assert.NotNull(xml);
                Assert.Equal("<div>Hello</div>", xml.ToString());
            }
        }
    }
}
