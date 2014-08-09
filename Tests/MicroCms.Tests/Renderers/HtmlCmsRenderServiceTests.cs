using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Renderers;
using Xunit;

namespace MicroCms.Tests.Renderers
{
    public class HtmlCmsRenderServiceTests : CmsUnityTests
    {
        [Fact]
        public void ValidateBasicRender()
        {
            using (var context = CreateContext())
            {
                var result = new HtmlCmsRenderService().Render(context, new CmsPart(CmsTypes.Html, "<html><head></head><body><h1>Hello, HTML</h1></body></html>"));
                Assert.NotNull(result);
            }
        }
         
    }
}
