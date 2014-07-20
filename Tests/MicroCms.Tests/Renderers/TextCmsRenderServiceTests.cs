using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Renderers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Tests.Renderers
{
    [TestClass]
    public class TextCmsRenderServiceTests
    {
        [TestMethod]
        public void ValidateBasicRender()
        {
            var result = new TextCmsRenderService().Render(new CmsPart(CmsTypes.Text, "Hello, World"));
            Assert.IsNotNull(result);
        }
         
    }
}
