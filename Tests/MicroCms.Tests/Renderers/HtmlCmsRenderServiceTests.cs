using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using MicroCms.Renderers;
using Xunit;

namespace MicroCms.Tests.Renderers
{
    public class HtmlCmsRenderServiceTests : CmsRenderServiceTests<HtmlCmsRenderService>
    {
        protected override string ContentType { get { return CmsTypes.Html; } }

        protected override HtmlCmsRenderService CreateRenderer()
        {
            return new HtmlCmsRenderService();
        }

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
