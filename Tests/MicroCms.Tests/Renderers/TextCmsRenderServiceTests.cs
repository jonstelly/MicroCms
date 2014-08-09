using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Renderers;
using Xunit;

namespace MicroCms.Tests.Renderers
{
    public class TextCmsRenderServiceTests : CmsUnityTests
    {
        [Fact]
        public void ValidateBasicRender()
        {
            using (var context = CreateContext())
            {
                var result = new TextCmsRenderService().Render(context, new CmsPart(CmsTypes.Text, "Hello, World"));
                Assert.NotNull(result);
            }
        }
         
    }
}
