using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroCms.Views;
using Xunit;

namespace MicroCms.Tests
{
    public class CmsViewTests : CmsUnityTests
    {
        [Fact]
        public void RenderSucceeds()
        {
            using (var context = CreateContext())
            {
                var view = new CmsContentView("example");
                var xml = view.Render(context, new CmsDocument("doc", new CmsPart(CmsTypes.Text, "Hello"))).Single();
                Assert.NotNull(xml);
                Assert.Equal("<div>Hello</div>", xml.ToString());
            }
        }
    }
}
